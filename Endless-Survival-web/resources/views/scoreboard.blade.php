@extends('layout')
@section('content')

        <div class="container pb-3">
            <h1 class="text-center display-6 py-3">{{ __('Ranking', [], $locale.'-plus') }}</h1>
            <table class="table table-striped bg-light">
                <tr class="bg-dark">
                    <th class="text-secondary fw-normal">#</th>
                    <th class="text-secondary fw-normal">{{ __('User', [], $locale.'-plus') }}</th>
                    <th class="text-secondary fw-normal"><p class="text-center m-0">{{ __('Best time', [], $locale.'-plus') }}</p></th>
                    <th class="text-secondary fw-normal"><p class="text-center m-0">{{ __('Matches', [], $locale.'-plus') }}</p></th>
                    <th class="text-secondary fw-normal"><p class="text-center m-0">{{ __('Play time', [], $locale.'-plus') }}</p></th>
                </tr>
                @foreach ($result as $row)
                    <tr>
                        <td style="width: 30px;"><p class="text-center m-0 mt-2">{{ $row->rank }}.</p></td>
                        <td class="my-text-ellipsis">
                            <a href="/profile/{{ str_replace(' ', '%20', $row->username) }}">
                                <img class="img img-fluid ml-2 rounded-circle profile @if($row->blur_off) {{ 'blur-off' }} @endif" src="/img/avatar/{{ $row->avatar }}" alt="{{ $row->avatar }}.jpg" title="{{ $row->username }}" width="40" height="40">
                                <span class="profile-link" title="{{ $row -> username }}">{{ $row -> username }}</span>
                            </a>
                        </td>
                        <td><p class="text-center m-0 mt-2">{{ $row->best_time }}</p></td>
                        <td><p class="text-center m-0 mt-2">{{ $row->matches }}</p></td>
                        <td><p class="text-center m-0 mt-2">{{ $row->play_time }}</p></td>
                    </tr>
                @endforeach
            </table>
        </div>

@endsection