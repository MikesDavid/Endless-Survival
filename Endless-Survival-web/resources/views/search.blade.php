@extends('layout')
@section('content')

        <div class="container pb-3">
            <h1 class="text-center display-6 py-3">{{ __('Search', [], $locale.'-plus') }}</h1>
            <form action="/search" method="get" class="d-flex d-md-none mb-3">
                <input class="form-control me-2" type="search" name="search" placeholder="{{ ucfirst(__('validation.attributes.username', [], $locale)) }}" aria-label="Search" @isset($search) value="{{ $search }}" @endisset>
                <button class="btn btn-primary my-btn-primary" type="submit">{{ __('Search', [], $locale.'-plus') }}</button>
            </form>
            <table class="table table-striped bg-light">
                <tr class="bg-dark">
                    <th class="text-secondary fw-normal">&ensp;{{ __('User', [], $locale.'-plus') }}</th>
                    <th class="text-secondary fw-normal"><p class="text-center m-0">{{ __('Best time', [], $locale.'-plus') }}</p></th>
                    <th class="text-secondary fw-normal"><p class="text-center m-0">{{ __('Matches', [], $locale.'-plus') }}</p></th>
                    <th class="text-secondary fw-normal"><p class="text-center m-0">{{ __('Play time', [], $locale.'-plus') }}</p></th>
                </tr>
                @if (Count($result) > 0)
                    @foreach ($result as $row)
                        <tr>
                            <td class="my-text-ellipsis">
                                <a href="/profile/{{ str_replace(' ', '%20', $row->username) }}">
                                    <img class="img img-fluid ml-2 rounded-circle profile @if($row->blur_off) {{ 'blur-off' }} @endif" src="/img/avatar/{{ $row->avatar }}" alt="{{ $row->avatar }}.jpg" title="{{ $row->username }}" width="40" height="40">
                                    <span class="profile-link" title="{{ $row -> username }}">{{ $row -> username }}</span>
                                </a>
                            </td>
                            <td><p class="text-center m-0 mt-2">{{ $row->best_time != null ? $row->best_time : '-' }}</p></td>
                            <td><p class="text-center m-0 mt-2">{{ $row->matches }}</p></td>
                            <td><p class="text-center m-0 mt-2">{{ $row->play_time != null ? $row->play_time : '00:00:00'  }}</p></td>
                        </tr>
                    @endforeach
                @else
                    <tr>
                        <td colspan="4"><p class="text-center m-0 mt-2 mb-2">{{ __('No result.', [], $locale.'-plus') }}</p></td>
                    </tr>
                @endif
            </table> 
        </div>

@endsection