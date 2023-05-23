<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\User;
use Illuminate\Support\Facades\Session;
use Illuminate\Support\Facades\Auth;
use Carbon\Carbon;
use Carbon\CarbonPeriod;

class PageController extends Controller
{
    function Home(){
        return redirect('/');
    }

    function Profile($username){
        $user = (User::where('username', '=', $username) -> get())[0];
        $avatar = $user -> avatar;
        $resolution = getimagesize('img/avatar/'.$avatar);
        $locale = app()->getLocale();

        /*svg*/ {
            $user_data = User::
            select('matches.*', 'characters.name')
                ->join('characters', 'users.id', '=', 'characters.uid')
                ->join('matches', 'characters.id', '=', 'matches.cid')
                ->where('users.id', '=', $user->id)
                ->orderby('date');
            
            if (Count($user_data->get()) > 0) {
                $months = [
                    __('Jan', [], $locale.'-plus'),
                    __('Feb', [], $locale.'-plus'),
                    __('Mar', [], $locale.'-plus'),
                    __('Apr', [], $locale.'-plus'),
                    __('May', [], $locale.'-plus'),
                    __('June', [], $locale.'-plus'),
                    __('July', [], $locale.'-plus'),
                    __('Aug', [], $locale.'-plus'),
                    __('Sept', [], $locale.'-plus'),
                    __('Oct', [], $locale.'-plus'),
                    __('Nov', [], $locale.'-plus'),
                    __('Dec', [], $locale.'-plus')
                ];

                $min_y = 260 / 100 * 86;
                $max_x = 450;

                $max_date = $user_data->max('date');
                $max_date = explode(' ', $max_date);
                $max_time = explode(':', $max_date[1]);
                $max_date_sec = (int)$max_time[0] * 3600 + (int)$max_time[1] * 60 + (int)$max_time[2];

                $min_date = $user->created_at;
                $min_date = explode(' ', $min_date);
                $min_time = explode(':', $min_date[1]);
                $min_date = explode('-', $min_date[0]);
                $min_date_sec = (int)$min_time[0] * 3600 + (int)$min_time[1] * 60 + (int)$min_time[2];

                $registrated_at = $user->created_at;
                if (Count($user_data->get()) > 0) {
                    $date_ranges = CarbonPeriod::create($registrated_at->format('Y-m-d'), $user_data->max('date'));
                    $dates = $date_ranges->toArray();
                }
                else
                    $dates = array($user_data->max('date'));

                $date_labels = array();
                $start = '';
                for ($i=0; $i < Count($dates); $i++) { 
                    $date = explode(' ', $dates[$i]);
                    $time = explode(':', $date[1]);
                    $date = explode('-', $date[0]);

                    if (Count($dates) > 365) {
                        if ($date[0] != $start) {
                            $start = $date[0];
                            $label = ['x' => $max_x / Count($dates) * ($i + 1 - ($i == 0 ? $min_date[2] : 0)), 'text' => $date[0]];
                            array_push($date_labels, $label);
                        }
                    }
                    else if (Count($dates) > 30) {
                        if ($date[1] != $start) {
                            $start = $date[1];
                            $label = ['x' => $max_x / Count($dates) * ($i + 1 - ($i == 0 ? $min_date[2] : 0)), 'text' => $months[(int)$date[1] - 1]];
                            array_push($date_labels, $label);
                        }
                    }
                    else if (Count($dates) > 1) {
                        if ($date[2] != $start) {
                            $start = $date[2];
                            $label = ['x' => $max_x / (Count($dates) - 1) * ($i - ($i == 0 ? ($min_date[2] != 0 ? 1 / $min_date[2]: 0) : 0)), 'text' => $months[(int)$date[1] - 1].' '.(int)$date[2].'.'];
                            array_push($date_labels, $label);
                        }
                    }
                    else {
                        for ($h=(int)explode(':', explode(' ', $registrated_at)[1])[0] - 1; $h < $max_time[0]; $h++) {
                            $label = ['x' => $max_x / Count($dates) / $max_time[0] * $h, 'text' => $h.':00'];
                            array_push($date_labels, $label);
                        }
                    }
                }

                while (Count($date_labels) > 11) {
                    $i = 0;
                    foreach ($date_labels as $date) {
                        $i++;
                        if ($i % 2 == 0)
                            unset($date_labels[array_search($date, $date_labels)]);
                    }
                }

                $svg_data = $user_data->get();
                $play_time = 0;
                for ($i=0; $i < Count($svg_data); $i++) {
                    $time = $svg_data[$i]->time;
                    $time = explode(':', $time);
                    $play_time += (int)$time[0] * 3600 + (int)$time[1] * 60 + (int)$time[2];

                    $svg_data[$i]['play_time'] = $play_time;
                }
                $max_y = $play_time;
                if ($max_y == 0) 
                    $max_y += 0.1;
            
                $max_range = $max_x / ((Count($dates) - 1) * 86400 + $max_date_sec);
                $dates_index = 0;
                for ($i=0; $i < Count($svg_data); $i++) {
                    $date = $svg_data[$i]->date;
                    $date = explode(' ', $date);
                    $time = explode(':', $date[1]);
                    $time = (int)$time[0] * 3600 + (int)$time[1] * 60 + (int)$time[2];
                    $play_time = $svg_data[$i]->play_time;
                    if ($play_time == 0) 
                        $play_time += 0.1;

                    while (explode(' ', $dates[$dates_index])[0] != explode(' ', $svg_data[$i]->date)[0] && $dates_index < Count($dates) - 1)
                        $dates_index += 1;

                    $svg_data[$i]['play_time'] = 
                        ($play_time >= 3600 ? floor($play_time / 3600).__('h', [], $locale.'-plus').' ' : '').
                        ($play_time >= 60 ? floor(($play_time / 60) % 60).__('m', [], $locale.'-plus').' ' : '').
                        ($play_time % 60).__('s', [], $locale.'-plus');
                    $svg_data[$i]['pos_x'] = ($max_range * ($dates_index) * 86400) + ($max_range * $time);
                    $svg_data[$i]['pos_y'] = $min_y - (float)$min_y / $max_y * $play_time;
                }
            }
        } //end svg

        /*scores*/ {
            $scores = 
                User::from(
                    User::from(
                        User::from(
                            User::from(
                                User::select(
                                    User::raw('
                                        users.id as id, 
                                        max(time) as best_time, 
                                        sec_to_time(sum(time_to_sec(time))) as play_time, 
                                        count(matches.id) as matches, 
                                        sum(kills) as kills, 
                                        sum(kills) / count(matches.id) as k_m, 
                                        sum(kills) / sum(death) as k_d, 
                                        sum(death) as death, 
                                        sum(damage_taken) as damage_taken
                                    ')
                                )
                                ->join('characters', 'users.id', '=', 'characters.uid')
                                ->join('matches', 'characters.id', '=', 'matches.cid')
                                ->groupby('users.id'),
                                'scores'
                            )
                            ->select(
                                User::raw('
                                    *, 
                                    DENSE_RANK() OVER(ORDER BY CASE WHEN best_time IS NULL THEN 1 END, best_time DESC) AS best_time_rank, 
                                    DENSE_RANK() OVER(ORDER BY CASE WHEN play_time IS NULL THEN 1 END, play_time DESC) AS play_time_rank, 
                                    DENSE_RANK() OVER(ORDER BY CASE WHEN matches IS NULL THEN 1 END, matches DESC) AS matches_rank, 
                                    DENSE_RANK() OVER(ORDER BY CASE WHEN kills IS NULL THEN 1 END, kills DESC) AS kills_rank, 
                                    DENSE_RANK() OVER(ORDER BY CASE WHEN k_m IS NULL THEN 1 END, k_m DESC) AS k_m_rank, 
                                    DENSE_RANK() OVER(ORDER BY CASE WHEN k_d IS NULL THEN 1 END, k_d DESC) AS k_d_rank
                                ')
                            ), 
                            'ranks'
                        )
                        ->select(
                            User::raw('
                                *, 
                                best_time_rank + play_time_rank + matches_rank + kills_rank + k_m_rank + k_d_rank as rank_point
                            ')
                        ), 
                        'sum_rank'
                    )
                    ->select(
                        User::raw('
                            *, 
                            DENSE_RANK() OVER(ORDER BY CASE WHEN rank_point IS NULL THEN 1 END, rank_point ASC) AS rank
                        ')
                    ),
                    'rank'
                )
                ->where('id', '=', $user->id)
                ->first();

            if (!isset($scores->best_time_rank)) $scores["best_time_rank"] = '-';
            else $scores->best_time_rank = '#'.$scores->best_time_rank;
            if (!isset($scores->play_time_rank)) $scores["play_time_rank"] = '-';
            else $scores->play_time_rank = '#'.$scores->play_time_rank;
            if (!isset($scores->matches_rank)) $scores["matches_rank"] = '-';
            else $scores->matches_rank = '#'.$scores->matches_rank;
            if (!isset($scores->kills_rank)) $scores["kills_rank"] = '-';
            else $scores->kills_rank = '#'.$scores->kills_rank;
            if (!isset($scores->k_m_rank)) $scores["k_m_rank"] = '-';
            else $scores->k_m_rank = '#'.$scores->k_m_rank;
            if (!isset($scores->k_d_rank)) $scores["k_d_rank"] = '-';
            else $scores->k_d_rank = '#'.$scores->k_d_rank;
            if (!isset($scores->best_time)) $scores["best_time"] = '-';
            if (!isset($scores->play_time)) $scores["play_time"] = '00:00:00';
            if (!isset($scores->matches)) $scores["matches"] = 0;
            if (!isset($scores->kills)) $scores["kills"] = 0;
            if (!isset($scores->k_m)) $scores["k_m"] = '-';
            else $scores->k_m = number_format($scores->k_m, 2);
            if (!isset($scores->k_d)) $scores["k_d"] = '-';
            else $scores->k_d = number_format($scores->k_d, 2);
            if (!isset($scores->death)) $scores["death"] = 0;
            if (!isset($scores->damage_taken)) $scores["damage_taken"] = 0;
            $scores["th"] = $user->id;
        } //end scores

        return view('/profile', [
            'page' => '/profile',
            'avatar' => $avatar,
            'username' => $user -> username,
            'my' => Auth::check() && $username == Auth::user()->username,
            'blur_off' => $resolution[0] < 256 || $resolution[1] < 256,
            'svg_data' => isset($svg_data) ? $svg_data : null,
            'min_y' => isset($min_y) ? $min_y : null,
            'date_labels' => isset($date_labels) ? $date_labels : null,
            'registrated_at' => $user->created_at,
            'scores' => $scores
        ]);
    }

    function Logout(Request $request){
        Session::flush();
        Auth::logout();

        return redirect('/');
    }

    function Scoreboard(){
        $result = 
            User::from(
                User::from(
                    User::from(
                        User::from(
                            User::select(
                                User::raw('
                                    users.id as id, 
                                    max(time) as best_time, 
                                    sec_to_time(sum(time_to_sec(time))) as play_time, 
                                    count(matches.id) as matches, 
                                    sum(kills) as kills, 
                                    sum(kills) / count(matches.id) as k_m, 
                                    sum(kills) / sum(death) as k_d
                                ')
                            )
                            ->join('characters', 'users.id', '=', 'characters.uid')
                            ->join('matches', 'characters.id', '=', 'matches.cid')
                            ->groupby('users.id'),
                            'scores'
                        )
                        ->select(
                            User::raw('
                                scores.id, 
                                scores.best_time, 
                                scores.play_time, 
                                scores.matches, 
                                DENSE_RANK() OVER(ORDER BY CASE WHEN best_time IS NULL THEN 1 END, best_time DESC) AS best_time_rank, 
                                DENSE_RANK() OVER(ORDER BY CASE WHEN play_time IS NULL THEN 1 END, play_time DESC) AS play_time_rank, 
                                DENSE_RANK() OVER(ORDER BY CASE WHEN matches IS NULL THEN 1 END, matches DESC) AS matches_rank, 
                                DENSE_RANK() OVER(ORDER BY CASE WHEN kills IS NULL THEN 1 END, kills DESC) AS kills_rank, 
                                DENSE_RANK() OVER(ORDER BY CASE WHEN k_m IS NULL THEN 1 END, k_m DESC) AS k_m_rank, 
                                DENSE_RANK() OVER(ORDER BY CASE WHEN k_d IS NULL THEN 1 END, k_d DESC) AS k_d_rank
                            ')
                        ), 
                        'ranks'
                    )
                    ->select(
                        User::raw('
                            ranks.id, 
                            ranks.best_time, 
                            ranks.play_time, 
                            ranks.matches, 
                            best_time_rank + play_time_rank + matches_rank + kills_rank + k_m_rank + k_d_rank as rank_point
                        ')
                    ), 
                    'sum_rank'
                )
                ->select(
                    User::raw('
                        sum_rank.id, 
                        sum_rank.best_time, 
                        sum_rank.play_time, 
                        sum_rank.matches, 
                        DENSE_RANK() OVER(ORDER BY CASE WHEN rank_point IS NULL THEN 1 END, rank_point ASC) AS rank
                    ')
                ),
                'rank'
            )
            ->select(
                'rank.best_time', 
                'rank.play_time', 
                'rank.matches', 
                'rank.rank', 
                'users.avatar', 
                'users.username'
            )
            ->join('users', 'rank.id', '=', 'users.id')
            ->orderby('rank')
            ->get();

        for ($i=0; $i < $result -> count(); $i++) {
            $resolution = getimagesize('img/avatar/'.$result[$i] -> avatar);
            $result[$i] -> blur_off = $resolution[0] < 256 || $resolution[1] < 256;
        }

        return view('/scoreboard', [
            'result' => $result,
            'page' => '/scoreboard'
        ]);
    }

    function Search(Request $request){
        $search = $request -> search;
        
        $result = 
            User::from(
                User::select(
                    User::raw('
                        users.id as id, 
                        max(time) as best_time, 
                        sec_to_time(sum(time_to_sec(time))) as play_time, 
                        count(matches.id) as matches
                    ')
                )
                ->join('characters', 'users.id', '=', 'characters.uid', 'left outer')
                ->join('matches', 'characters.id', '=', 'matches.cid', 'left outer')
                ->groupby('users.id'),
                'scores'
            )
            ->select(
                'scores.best_time', 
                'scores.play_time', 
                'scores.matches',
                'users.avatar', 
                'users.username'
            )
            ->join('users', 'scores.id', '=', 'users.id')
            ->orderby('username')
            ->where('username', 'LIKE', '%'.$search.'%')
            ->get();

        for ($i=0; $i < $result -> count(); $i++) {
            $resolution = getimagesize('img/avatar/'.$result[$i] -> avatar);
            $result[$i] -> blur_off = $resolution[0] < 256 || $resolution[1] < 256;
        }

        return view('/search', [
            'result' => $result,
            'search' => $search,
            'page' => '/search'
        ]);
    }

    function Edit(){
        $user = Auth::user();

        $message = session()->get('message');

        return view('/edit', [
            'page' => '/profile',
            'my' => true,
            'user' => $user,
            'blur_off' => session()->get('blur_off'),
            'message' => isset($message) ? $message : null,
            'email_verified_at' => $user->email_verified_at != null ? Carbon::parse($user->email_verified_at)->format('Y-m-d H:i:s') : null
        ]);
    }

    function ChangeLanguage($locale){
        session()->put('locale', $locale);

        return back();
    }
}
