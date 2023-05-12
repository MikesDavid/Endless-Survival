@extends('layout')
@section('content')

        <div class="container pb-3">
            <div class="card m-auto my-3" style="width: 25rem;">
                <div class="card-body">
                    <h1 class="text-center display-6 mb-3">{{ __('Login', [], $locale) }}</h1>
                    <form action="login" method="post">
                        @csrf
                        @isset($error)
                            <p class="text-danger">{{ $error }}</p>
                            @php
                                $error_username = 'border-danger';
                                $error_password = 'border-danger';
                            @endphp
                        @endisset
                        <div class="mb-3">
                            @error('username')
                                @php($error_username = 'border-danger')
                            @enderror
                            <input class="form-control @isset($error_username) {{ $error_username }} @endisset" type="text" name="username" placeholder="{{ __('Email address or username', [], $locale.'-plus') }}" value="{{ old('username') }}">
                            @error('username')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            @error('password')
                                @php($error_password = 'border-danger')
                            @enderror
                            <input class="form-control @isset($error_password) {{ $error_password }} @endisset" type="password" name="password" placeholder="{{ ucfirst(__('validation.attributes.password', [], $locale)) }}">
                            @error('password')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            <input class="form-check-input" type="checkbox" name="remember">
                            <label class="form-check-label" for="remember">{{ __('Stay logged in', [], $locale.'-plus') }}</label>
                        </div>
                        <div class="mb-3 text-center">
                            <button class="btn btn-primary my-btn-primary fw-bold container-fluid p-2" type="submit">{{ __('Login', [], $locale) }}</button>
                        </div>
                        <div class="mb-3 text-center">
                            <a href="/forgot-password">{{ __('Forgot your password?', [], $locale.'-plus') }}</a>
                            <hr>
                        </div>
                        <div class="text-center">
                            <a class="btn btn-success my-btn-success fw-bold container-fluid p-2" href="/registration">{{ __('Registration', [], $locale.'-plus') }}</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>

@endsection