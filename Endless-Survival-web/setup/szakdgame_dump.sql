-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2023. Máj 12. 19:27
-- Kiszolgáló verziója: 10.4.28-MariaDB
-- PHP verzió: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `szakdgame`
--
CREATE DATABASE IF NOT EXISTS `szakdgame` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE `szakdgame`;

DELIMITER $$
--
-- Eljárások
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_new_match` (IN `user_id` BIGINT(20), IN `character_name` VARCHAR(50), IN `time` TIME, IN `kills` INT, IN `death` BIT, IN `primary_weapon` VARCHAR(50), IN `secondary_weapon` VARCHAR(50), IN `damage_taken` INT, IN `date` DATETIME)   begin
	DECLARE user_created_at timestamp;
    DECLARE character_count int;
    DECLARE character_id int;
    
    SELECT COUNT(*), characters.id INTO character_count, character_id FROM users 
		INNER JOIN characters ON users.id = characters.uid
		WHERE users.id = user_id
        AND characters.name = character_name;
          
    SELECT created_at INTO user_created_at FROM users
    	WHERE users.id = user_id;
    IF date < user_created_at THEN
    	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Match date may not be earlier than the time of the user\'s registration!';
    END IF;
    
    IF character_count < 1 THEN
    	INSERT INTO characters(uid, name)
        	VALUES(
                user_id,
                character_name);
                
        SELECT characters.id INTO character_id FROM users
			INNER JOIN characters ON users.id = characters.uid
			WHERE users.id = user_id
        	AND characters.name = character_name;
    END IF;
    
	INSERT INTO matches(cid, time, kills, death, primary_weapon, secondary_weapon, damage_taken, date) 
    	VALUES(
        	character_id,
          	time,
            kills,
            death,
            primary_weapon,
            secondary_weapon,
            damage_taken,
            date);
end$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `add_rand_match` (IN `user_username` VARCHAR(15), IN `add_count` INT)   begin
    DECLARE user_id int;
    DECLARE user_created_at timestamp;
    DECLARE character_count int;
    DECLARE character_id int;
    DECLARE i int;
    
    SELECT id, created_at INTO user_id, user_created_at FROM users
    	WHERE users.username = user_username;
        
    IF user_id IS NULL THEN
    	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'There is no such username in the database!';
    END IF;
    IF add_count < 0 THEN
    	SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'You can\'t add less than 0 match!';
    END IF;
    
    SELECT COUNT(*), characters.id INTO character_count, character_id FROM users 
		INNER JOIN characters ON users.id = characters.uid
		WHERE users.username = user_username
        AND characters.name = 'test';
    
    IF character_count < 1 THEN
    	INSERT INTO characters(uid, name)
        	VALUES(
                user_id,
                'test');
                
        SELECT characters.id INTO character_id FROM users
			INNER JOIN characters ON users.id = characters.uid
			WHERE users.username = user_username
        	AND characters.name = 'test';
    END IF;
    
    SET i = 0;
    add_match: LOOP
    	IF i < add_count THEN
			INSERT INTO matches(cid, time, kills, death, primary_weapon, secondary_weapon, damage_taken, date) 
    			VALUES(
        			character_id,
          			SEC_TO_TIME(FLOOR(RAND()*(TIME_TO_SEC('01:30:00')-TIME_TO_SEC('00:00:30')+1)+TIME_TO_SEC('00:00:30'))),
            		FLOOR(RAND()*(30-0+1)+0),
            		FLOOR(RAND()*(1-0+1)+0),
            		'Aka-47',
            		'Knife',
            		FLOOR(RAND()*(3000-0+1)+0),
            		TIMESTAMPADD(SECOND, FLOOR(RAND()*TIMESTAMPDIFF(SECOND, user_created_at, CURRENT_TIMESTAMP())), user_created_at));
        	SET i = i + 1;
            ITERATE add_match;
        END IF;
        LEAVE add_match;
	END LOOP add_match;
    
    SELECT i as added_matches;
end$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `characters`
--

CREATE TABLE `characters` (
  `id` int(11) NOT NULL,
  `uid` bigint(20) UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL,
  `level` int(11) NOT NULL DEFAULT 0,
  `caste` varchar(100) DEFAULT NULL,
  `save` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `matches`
--

CREATE TABLE `matches` (
  `id` int(11) NOT NULL,
  `cid` int(11) NOT NULL,
  `time` time NOT NULL,
  `kills` int(11) NOT NULL,
  `death` bit(1) NOT NULL,
  `primary_weapon` varchar(50) NOT NULL,
  `secondary_weapon` varchar(50) NOT NULL,
  `damage_taken` int(11) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `migrations`
--

CREATE TABLE `migrations` (
  `id` int(10) UNSIGNED NOT NULL,
  `migration` varchar(255) NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '2014_10_12_000000_create_users_table', 1),
(2, '2014_10_12_100000_create_password_resets_table', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `password_resets`
--

CREATE TABLE `password_resets` (
  `email` varchar(255) NOT NULL,
  `token` varchar(255) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `username` varchar(15) NOT NULL,
  `password` varchar(255) NOT NULL,
  `avatar` text NOT NULL DEFAULT 'default.jpg',
  `email_verified_at` timestamp NULL DEFAULT NULL,
  `remember_token` varchar(100) DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `characters`
--
ALTER TABLE `characters`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_character` (`uid`);

--
-- A tábla indexei `matches`
--
ALTER TABLE `matches`
  ADD PRIMARY KEY (`id`),
  ADD KEY `character_match` (`cid`);

--
-- A tábla indexei `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `password_resets`
--
ALTER TABLE `password_resets`
  ADD PRIMARY KEY (`email`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `email` (`email`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `characters`
--
ALTER TABLE `characters`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `matches`
--
ALTER TABLE `matches`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `characters`
--
ALTER TABLE `characters`
  ADD CONSTRAINT `user_character` FOREIGN KEY (`uid`) REFERENCES `users` (`id`);

--
-- Megkötések a táblához `matches`
--
ALTER TABLE `matches`
  ADD CONSTRAINT `character_match` FOREIGN KEY (`cid`) REFERENCES `characters` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
