@php
  if (Auth::check())
  {
    $avatar = Auth::user()->avatar;
    $username = Auth::user()->username;
    $profile_link = 'profile/'.Auth::user()->username;
    $blur_off = session()->get('blur_off');
    if (!isset($my))
      $my = false;
  }
  else
  {
    $profile_link = 'login';
  }
  if (!isset($page))
    $page = '';
@endphp
<!DOCTYPE html>
<html lang="{{ strtoupper($locale).'-'.$locale }}">
    <head>
        <meta charset="UTF-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>{{ env('APP_NAME') }}</title>
        <link rel="stylesheet" href="{{asset('css/bootstrap.css')}}">
        <link rel="stylesheet" href="{{asset('css/mystyle.css')}}">
        <script src="{{asset('js/bootstrap.bundle.js')}}"></script>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
        <script src="{{asset('js/my.js')}}"></script>
        <link rel="shortcut icon" href="{{asset('img/favicon.ico')}}" type="image/x-icon">
    </head>
    <body>
      <div class="bg-dark">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark container">
            <div class="container-fluid">
              <a class="navbar-brand p-0" href="/"><img src="/img/icon.png" alt="icon.jpg" title="{{ __('Main page', [], $locale.'-plus') }}" width="40" height="40" class="blur-off"></a>
              <a href="/{{ str_replace(' ', '%20', $profile_link) }}" class="d-inline d-md-none">
                @auth
                  <img class="img img-fluid rounded-circle my-profile-center my-img-fit-cover @if($page == '/profile' && $my)) {{ 'my-profile-active-center' }} @endif @if($blur_off) {{ 'blur-off' }} @endif" src="/img/avatar/{{ $avatar }}" alt="{{ $avatar }}" title="{{ $username }}" width="40" height="40"> 
                @else
                  <i class="my-fa fa fa-user-circle fa-2x ms-2 @if($page == '/login') {{ 'text-white' }} @endif" aria-hidden="true" title="{{ __('Login', [], $locale) }}"></i>
                @endauth
              </a>
              <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
              </button>
              <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="navbar-nav me-auto mb-2 mb-md-0">
                  <li class="nav-item">
                    <a class="nav-link @if($page == '/') {{ 'active' }} @endif" aria-current="page" href="/">{{ env('APP_NAME') }}</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link @if($page == '/scoreboard') {{ 'active' }} @endif" href="/scoreboard">{{ __('Ranking', [], $locale.'-plus') }}</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link  @if($page == '/about') {{ 'active' }} @endif" href="/about">{{ __('About', [], $locale.'-plus') }}</a>
                  </li>
                  <li class="nav-item d-md-none">
                    <a class="nav-link  @if($page == '/search') {{ 'active' }} @endif" href="/search">{{ __('Search', [], $locale.'-plus') }}&ensp;<i class="fa fa-search" aria-hidden="true"></i></a>
                  </li>
                </ul>
                <form action="/search" method="get" class="d-none d-md-inline-flex">
                  <input class="form-control me-2" type="search" name="search" placeholder="{{ ucfirst(__('validation.attributes.username', [], $locale)) }}" aria-label="Search" @isset($search) value="{{ $search }}" @endisset>
                  <button class="btn btn-primary" type="submit">{{ __('Search', [], $locale.'-plus') }}</button>
                </form>
                <a href="/{{ str_replace(' ', '%20', $profile_link) }}" class="d-none d-md-inline">
                  @auth
                    <img class="img img-fluid rounded-circle my-profile my-img-fit-cover @if($page == '/profile' && $my)) {{ 'my-profile-active' }} @endif @if($blur_off) {{ 'blur-off' }} @endif" src="/img/avatar/{{ $avatar }}" alt="{{ $avatar }}" title="{{ $username }}" width="40" height="40">
                  @else
                    <i class="my-fa fa fa-user-circle fa-2x ms-2 @if($page == '/login') {{ 'text-white' }} @endif" aria-hidden="true" title="{{ __('Login', [], $locale) }}"></i>
                  @endauth
                </a>
              </div>
            </div>
        </nav>
      </div>
      <div class="content">
        @yield('content')
      </div>
      <footer class="text-center ps-0 pe-0 bg-dark text-white">
        <div class="container pt-3">
          <div class="row">
            <div class="col-4 text-start">
              <div class="row">
                <div class="col-auto pe-0">
                  <p>{{ __('Language', [], $locale.'-plus') }}:</p>
                </div>
                <div class="col">
                  <p>
                    @php ($i = 1)
                    @foreach (config('app.locales') as $short => $long)
                      <a class="@if($locale == $short) text-decoration-underline @endif" href="/change-language/{{ $short }}">{{ __($long, [], $locale.'-plus')}}</a>@if ($i < Count(config('app.locales'))),@endif
                      @php ($i += 1)
                    @endforeach
                  </p>
                </div>
              </div>
            </div>
            <div class="col-4 text-center">
              <p>{{ env('APP_NAME') }}</p>
            </div>
            <div class="col-4 text-end">
              <p>{{ ucfirst(__('validation.attributes.email', [], $locale.'-plus')) }}: <span class="ps-2">{{ env('MAIL_USERNAME') }}</span></p>
            </div>
          </div>
        </div>
      </footer>
    </body>
</html>