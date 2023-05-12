@extends('layout')
@section('content')

        <div class="container pb-3">
            <div class="card m-auto my-3" style="width: 25rem;">
                <div class="card-body">
                    <h1 class="text-center display-6 mb-3">{{ __('Registration', [], $locale.'-plus') }}</h1>
                    <form action="registration" method="post">
                        @csrf
                        <div class="mb-3">
                            @error('email')
                                @php($error_email = 'border-danger')
                            @enderror
                            <label class="form-label" for="email">{{ ucfirst(__('validation.attributes.email', [], $locale)) }}:</label>
                            <input class="form-control @isset($error_email) {{ $error_email }} @endisset" type="email" name="email" value="{{ old('email') }}">
                            @error('email')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            @error('username')
                                @php($error_username = 'border-danger')
                            @enderror
                            <label class="form-label" for="username">{{ ucfirst(__('validation.attributes.username', [], $locale)) }}:</label>
                            <input class="form-control @isset($error_username) {{ $error_username }} @endisset" type="text" name="username" value="{{ old('username') }}">
                            @error('username')
                            <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            @error('password')
                                @php($error_password = 'border-danger')
                            @enderror
                            <label class="form-label" for="password">{{ ucfirst(__('validation.attributes.password', [], $locale)) }}:</label>
                            <input class="form-control @isset($error_password) {{ $error_password }} @endisset" type="password" name="password" value="{{ old('password') }}">
                            @error('password')
                            <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            @error('password_confirmation')
                                @php($error_password_confirmation = 'border-danger')
                            @enderror
                            <label class="form-label" for="password_confirmation">{{ ucfirst(__('validation.attributes.password_confirmation', [], $locale)) }}:</label>
                            <input class="form-control @isset($error_password_confirmation) {{ $error_password_confirmation }} @endisset" type="password" name="password_confirmation" value="{{ old('password_confirmation') }}">
                            @error('password_confirmation')
                            <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="text-center">
                            <button class="btn btn-primary my-btn-primary fw-bold container-fluid p-2" type="submit">{{ __('Registration', [], $locale.'-plus') }}</button>
                        </div>
                        <div class="text-center">
                            <hr>
                            <a class="btn btn-success my-btn-success fw-bold container-fluid p-2" href="/login">{{ __('Login', [], $locale) }}</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>

@endsection