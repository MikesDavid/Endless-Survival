@extends('layout')
@section('content')

        <div class="container pb-3">
            <div class="card m-auto my-3" style="width: 25rem;">
                <div class="card-body">
                    <h1 class="text-center display-6 mb-3">{{ __('Delete Account', [], $locale.'-plus') }}</h1>
                    <form action="delete-auth" method="post">
                        @csrf
                        <div class="mb-3">
                            @if($errors->has('password') || isset($error))
                                @php($error_password = 'border-danger')
                            @endif
                            <input class="form-control @isset($error_password) {{ $error_password }} @endisset" type="password" name="password" placeholder="{{ __('Enter your password', [], $locale.'-plus') }}">
                            @error('password')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                            @if (isset($error))
                                <p class="text-danger">{{ $error }}</p>
                            @endif
                        </div>
                        <div class="mb-3 text-center row">
                            <div class="col pe-1">
                                <a class="btn btn-success my-btn-success fw-bold container-fluid p-2" href="/profile/{{ Auth::user()->username }}">{{ __('Cancel', [], $locale.'-plus') }}</a>
                            </div>
                            <div class="col ps-1">
                                <button class="btn btn-danger my-btn-danger fw-bold container-fluid p-2" type="submit">{{ __('Delete', [], $locale.'-plus') }}</button>
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