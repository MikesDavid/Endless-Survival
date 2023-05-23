@extends('layout')
@section('content')

        <div class="container pb-3">
            <h1 class="text-center display-6 py-3">{{ __('About the game', [], $locale.'-plus') }}</h1>
            <p class="fs-5">{{ __('Endless Survival is a single player survival shooter game (multiplayer option is planned in the future). During the game, the player can choose a primary and a secondary weapon before each level. Players receive experience points after each course. If enough experience points are collected, the player levels up and receives a skill point. Skill points can be spent off-course on various character power-ups or abilities. In addition to skill points, new weapons become available when certain levels are reached.', [], $locale.'-plus') }}</p>
            <p class="fs-5">{{ __('The game is a desktop application written in C#, running under the Windows operating system. The game uses the Unity engine.', [], $locale.'-plus') }}</p>
            <br>
            <p class="fs-5">{{ __('The goal of the game is for the player to try to survive as long as possible against enemies that are constantly getting stronger and spawning faster and faster.', [], $locale.'-plus') }}</p>
            <p class="text-center"><a class="btn btn-success my-btn-success text-white fw-bold" href="https://github.com/MikesDavid/Game" target="_blank">{{ __('Download', [], $locale.'-plus') }}</a></p>
        </div>

@endsection