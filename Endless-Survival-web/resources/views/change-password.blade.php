@extends('layout')
@section('content')

        <div class="container pb-3">
            <div class="card m-auto my-3" style="width: 25rem;">
                <div class="card-body">
                    <h1 class="text-center display-6 mb-3">{{ __('Change password', [], $locale.'-plus') }}</h1>
                    <form action="change-password" method="post">
                        @csrf
                        <div class="mb-3">
                            @if($errors->has('password') || isset($error))
                                @php($error_password = 'border-danger')
                            @endif
                            <input class="form-control @isset($error_password) {{ $error_password }} @endisset" type="password" name="password" placeholder="{{ __('Enter your old password', [], $locale.'-plus') }}">
                            @error('password')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                            @if (isset($error))
                                <p class="text-danger">{{ $error }}</p>
                            @endif
                        </div>
                        <div class="mb-3">
                            @if($errors->has('new_password'))
                                @php($error_new_password = 'border-danger')
                            @endif
                            <input class="form-control @isset($error_new_password) {{ $error_new_password }} @endisset" type="password" name="new_password" placeholder="{{ __('Enter your new password', [], $locale.'-plus') }}">
                            @error('new_password')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            @if($errors->has('new_password_confirmation'))
                                @php($error_new_password_confirmation = 'border-danger')
                            @endif
                            <input class="form-control @isset($error_new_password_confirmation) {{ $error_new_password_confirmation }} @endisset" type="password" name="new_password_confirmation" placeholder="{{ __('Enter your new password again', [], $locale.'-plus') }}">
                            @error('new_password_confirmation')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3 text-center row">
                            <div class="col pe-1">
                                <a class="btn btn-danger my-btn-danger fw-bold container-fluid p-2" href="/profile/{{ Auth::user()->username }}">{{ __('Cancel', [], $locale.'-plus') }}</a>
                            </div>
                            <div class="col ps-1">
                                <button class="btn btn-success my-btn-success fw-bold container-fluid p-2" type="submit">{{ __('Change', [], $locale.'-plus') }}</button>
                            </div>
                        </div>
                        <div class="text-center">
                            <hr>
                            <a href="/forgot-password">{{ __('Forgot your password?', [], $locale.'-plus') }}</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>

@endsection