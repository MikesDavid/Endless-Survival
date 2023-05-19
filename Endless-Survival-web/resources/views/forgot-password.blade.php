@extends('layout')
@section('content')

        <div class="container pb-3">
            <div class="card m-auto my-3" style="width: 25rem;">
                <div class="card-body">
                    <h1 class="text-center display-6 mb-3">{{ __('Reset Password', [], $locale) }}</h1>
                    <form action="forgot-password" method="post">
                        @csrf
                        <div class="mb-3">
                            @error('username')
                                @php($error = 'border-danger')
                            @enderror
                            <input class="form-control @isset($error) {{ $error }} @endisset" type="text" name="username" placeholder="{{ __('Email address or username', [], $locale.'-plus') }}" value="{{ old('username') }}">
                            @error('username')
                                <p class="text-danger">{{ $message }}</p>
                            @enderror
                        </div>
                        <div class="text-center row">
                            <div class="col pe-1">
                                <a class="btn btn-danger my-btn-danger fw-bold container-fluid p-2" href="{{ url()->previous() }}">{{ __('Back', [], $locale.'-plus') }}</a>
                            </div>
                            <div class="col ps-1">
                                <button class="btn btn-success my-btn-success fw-bold container-fluid p-2" type="submit">{{ __('Send', [], $locale.'-plus') }}</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            @if (isset($message))
                <div class="pb-2 mt-2 mx-1 my-bg-green-gradient rounded">
                    <div class="text-center mt-2">
                        <p class="mb-0">{{ $message }}</p>
                    </div>
                </div>
            @endif
        </div>

@endsection