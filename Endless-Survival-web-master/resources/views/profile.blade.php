@extends('layout')
@section('content')

        <div class="container pb-3 ps-0 pe-0">
            <div class="row mt-3 p-2 ms-1 me-1 my-bg-gray-gradient rounded">
                <div class="col-4 p-0">
                    <div class="card p-0 bg-transparent border-0">
                        @if (isset($scores->rank))
                            <p class="rank" title="{{ __('Rank', [], $locale.'-plus') }}"><span class="rounded">#{{ $scores->rank }}</span></p>
                        @endif
                        <div class="my-img-frame">
                            <img class="img img-fluid rounded-circle my-border-avatar bg-dark my-img @if($blur_off) {{ 'blur-off' }} @endif" src="/img/avatar/{{ $avatar }}" alt="{{ $avatar }}" title="{{ $username }}">
                        </div>
                        <div class="card-body p-0">
                            <p class="card-text text-center h4" title="{{ $username }}">{{ $username }}</p>
                        </div>
                    </div>
                </div>
                <div class="col-8 p-0 ps-2">
                    <div class="bg-dark my-border-svg" style="height: 100%; width: 100%;">
                        <div id="tooltip" style="position: absolute; display: none;">
                            <p>{{ __('Primary weapon', [], $locale.'-plus') }}: <span id="primary_weapon"></span></p>
                            <p>{{ __('Secondary weapon', [], $locale.'-plus') }}: <span id="secondary_weapon"></span></p>
                            <p>{{ __('Kills', [], $locale.'-plus') }}: <span id="kills"></span></p>
                            <p>{{ __('Deaths', [], $locale.'-plus') }}: <span id="death"></span></p>
                            <p>{{ __('Damage taken', [], $locale.'-plus') }}: <span id="damage_taken"></span></p>
                            <p>{{ __('Survival time', [], $locale.'-plus') }}: <span id="time"></span></p>
                            <p>{{ __('Date', [], $locale.'-plus') }}: <span id="date"></span></p>
                            <p class="fw-bold">{{ __('Play time', [], $locale.'-plus') }}: <span class="fw-normal" id="play_time"></span></p>
                        </div>
                        <div id="tooltip_reg" style="position: absolute; display: none;">
                            <p>{{ __('Registered', [], $locale.'-plus') }}: <br><span class="text-center">{{ $registrated_at }}</span></p>
                        </div>
                        <svg height="100%" width="100%" viewBox="0 0 450 260">
                            @if (isset($svg_data))
                                <circle id="0" cx="0" cy="86%" r="4" style="opacity: 0;"/>
                                @foreach ($date_labels as $label)
                                    <line x1="{{ $label['x'] }}" y1="0" x2="{{ $label['x'] }}" y2="86%" style="stroke:#333f33;stroke-width:2" />
                                    <text x="{{ $label['x'] - 17 }}" y="95%" fill="green">
                                        {{ $label['text'] }}
                                        <title>{{ $label['text'] }}</title>
                                    </text>
                                @endforeach
                                <polyline points="0,{{ $min_y }}@foreach ($svg_data as $match){{ ' '.$match->pos_x.','.$match->pos_y }}@endforeach" style="fill:none;stroke:#960202;stroke-width:2"/>
                                <rect class="h-100" x="0" y="0" width="10" onmouseenter="showTooltip();" onmouseout="hideTooltip();"/>
                                @foreach ($svg_data as $match)
                                    <circle id="{{ $match->id }}" cx="{{ $match->pos_x }}" cy="{{ $match->pos_y }}" r="0" style="opacity: 0;"/>
                                    <rect class="h-100" x="{{ $match->pos_x-5 }}" y="0" width="5" onmouseenter="showTooltip({{ $match }});" onmouseout="hideTooltip();"/>
                                @endforeach
                                <line id="line" x1="-10" y1="0%" x2="-10" y2="86%" style="stroke:#029602;stroke-width:2" />
                                <circle id="circle" cx="-10" cy="-10" r="4"/>
                            @else
                                @if (Auth::Check() && $username == Auth::user()->username)
                                    <text x="28%" y="50%" fill="red">{{ __("You haven't played a game yet", [], $locale.'-plus') }}</text>
                                @else
                                    <text x="20%" y="50%" fill="red" class="fs-3">{{ __("He hasn't played a game yet", [], $locale.'-plus') }}</text>
                                @endif
                            @endif
                            <line x1="0%" y1="86%" x2="100%" y2="86%" style="stroke:#029602;stroke-width:2" />
                        </svg>
                    </div>
                </div>
            </div>
            <div class="row mt-3 p-2 ms-1 me-1 my-bg-orange-gradient rounded text-center pt-3 pb-3">
                <div class="col my-border-orange d-flex align-items-center">
                    <p class="m-0 d-felx-1-0-0">{{ __('Best time', [], $locale.'-plus') }}: {{ $scores["best_time_rank"] }}</p>
                </div>
                <div class="col my-border-orange d-flex align-items-center">
                    <p class="m-0 d-felx-1-0-0">{{ __('Play time', [], $locale.'-plus') }}: {{ $scores["play_time_rank"] }}</p>
                </div>
                <div class="col d-flex align-items-center">
                    <p class="m-0 d-felx-1-0-0">{{ __('Matches', [], $locale.'-plus') }}: {{ $scores["matches_rank"] }}</p>
                </div>
            </div>
            <div class="row mt-3 p-2 ms-1 me-1 my-bg-orange-gradient rounded text-center pt-3 pb-3">
                <div class="col my-border-orange d-flex align-items-center">
                    <p class="m-0 d-felx-1-0-0">{{ __('Kills', [], $locale.'-plus') }}: {{ $scores["kills_rank"] }}</p>
                </div>
                <div class="col my-border-orange d-flex align-items-center">
                    <p class="m-0 d-felx-1-0-0">{{ __('K/M', [], $locale.'-plus') }}: {{ $scores["k_m_rank"] }}</p>
                </div>
                <div class="col d-flex align-items-center">
                    <p class="m-0 d-felx-1-0-0">{{ __('K/D', [], $locale.'-plus') }}: {{ $scores["k_d_rank"] }}</p>
                </div>
            </div>
            <div class="mt-3 ms-1 me-1 rounded my-bg-silver-gradient text-center">
                <div class="row p-2 ms-0 me-0 pt-3 pb-3 bottom">
                    <div class="col-4 my-border-silver d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('Best time', [], $locale.'-plus') }}: {{ $scores["best_time"] }}</p>
                    </div>
                    <div class="col-4 my-border-silver d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('Play time', [], $locale.'-plus') }}: {{ $scores["play_time"] }}</p>
                    </div>
                    <div class="col-4 d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('Matches', [], $locale.'-plus') }}: {{ $scores["matches"] }}</p>
                    </div>
                </div>
                <div class="row p-2 ms-0 me-0 pt-3 pb-3 bottom">
                    <div class="col-4 my-border-silver d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('Kills', [], $locale.'-plus') }}: {{ $scores["kills"] }}</p>
                    </div>
                    <div class="col-4 my-border-silver d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('K/M', [], $locale.'-plus') }}: {{ $scores["k_m"] }}</p>
                    </div>
                    <div class="col-4 d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('K/D', [], $locale.'-plus') }}: {{ $scores["k_d"] }}</p>
                    </div>
                </div>
                <div class="row p-2 ms-0 me-0 pt-3 pb-3">
                    <div class="col-4 my-border-silver d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('Deaths', [], $locale.'-plus') }}: {{ $scores["death"] }}</p>
                    </div>
                    <div class="col-4 my-border-silver d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ __('Damage taken', [], $locale.'-plus') }}: {{ $scores["damage_taken"] }}</p>
                    </div>
                    <div class="col-4 d-flex align-items-center">
                        <p class="m-0 d-felx-1-0-0">{{ $scores["th"].__('th player', [], $locale.'-plus') }}</p>
                    </div>
                </div>
            </div>
            @if ($my)
                <div class="ms-1 me-1">
                    <hr>
                </div>
                <div class="row mt-3 p-2 ms-1 me-1 text-center">
                    <div class="col">
                        <a class="btn btn-danger my-btn" href="/edit">{{ __('Edit profile', [], $locale.'-plus') }}</a>
                    </div>
                    <div class="col">
                        <a class="btn btn-danger my-btn" href="/change-password">{{ __('Change password', [], $locale.'-plus') }}</a>
                    </div>
                    <div class="col">
                        <a class="btn btn-danger my-btn" href="/delete-auth">{{ __('Delete Account', [], $locale.'-plus') }}</a>
                    </div>
                    <div class="col">
                       <a class="btn btn-danger my-btn" href="/logout">{{ __('Logout', [], $locale) }}</a>
                    </div>
                </div>
            @endif
        </div>

@endsection