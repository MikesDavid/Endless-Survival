@extends('layout')
@section('content')

        <div class="container pb-3">
            <div class="card m-auto my-3" style="width: 25rem;">
                <div class="card-body">
                    <h1 class="text-center display-6 mb-3">{{ __('Reset Password', [], $locale) }}</h1>
                    <form action="/reset-password" method="post">
                        @csrf
                        <input type="hidden" name="token", value="{{ $token }}">
                        <input type="hidden" name="email", value="{{ $_GET['email'] }}">
                        <div class="mb-3">
                            @error('password')
                                @php($error_password = 'border-danger')
                            @enderror
                            <input class="form-control @isset($error_password) {{ $error_password }} @endisset" type="password" name="password" placeholder="{{ ucfirst(__('validation.attributes.new_password', [], $locale)) }}">
                            @error('password')
                            <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="mb-3">
                            @error('password_confirmation')
                                @php($error_password_confirmation = 'border-danger')
                            @enderror
                            <input class="form-control @isset($error_password_confirmation) {{ $error_password_confirmation }} @endisset" type="password" name="password_confirmation" placeholder="{{ ucfirst(__('validation.attributes.new_password_confirmation', [], $locale)) }}">
                            @error('password_confirmation')
                            <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="text-center row">
                            <div class="col pe-1">
                                <a class="btn btn-danger my-btn-danger fw-bold container-fluid p-2" href="/">{{ __('Cancel', [], $locale.'-plus') }}</a>
                            </div>
                            <div class="col ps-1">
                                <button class="btn btn-success my-btn-success fw-bold container-fluid p-2" type="submit">{{ __('Reset', [], $locale.'-plus') }}</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

@endsection