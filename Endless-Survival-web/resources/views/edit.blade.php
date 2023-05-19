@extends('layout')
@section('content')

        <div class="container pb-3 ps-0 pe-0">
            <form id="email-resend" action="/email/verification-notification" method="post">@csrf</form>
            <form action="/edit" method="post" enctype="multipart/form-data">
                @csrf
                <div class="row mt-3 p-2 ms-1 me-1 my-bg-gray-gradient rounded">
                    <div class="col-4 p-0">
                        <div class="card p-0 bg-transparent border-0">
                            <div class="my-img-frame">
                                <img class="img img-fluid rounded-circle my-border-avatar bg-dark my-img @if($blur_off) {{ 'blur-off' }} @endif" src="/img/avatar/{{ $user->avatar }}" alt="{{ $user->avatar }}" title="{{ $user->username }}">
                            </div>
                            <div class="card-body p-0">
                                <p class="card-text text-center h4">{{ $user->username }}</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-8 p-0 ps-2">
                        <div class="bg-dark my-border-svg" style="height: 100%; width: 100%;">
                            <table class="text-success my-table-edit h-100">
                                <tr>
                                    <th>{{ ucfirst(__('validation.attributes.username', [], $locale)) }}:</th>
                                    <td><input class="text-success text-break ps-1 my-input-text" type="text" name="username" value="@if(!$errors->has('username')){{ $user->username }}@else{{ old('username') }}@endif"></td>
                                </tr>
                                <tr>
                                    <th>{{ ucfirst(__('validation.attributes.email', [], $locale)) }}:</th>
                                    <td class="text-break">{{ $user->email }}</td>
                                </tr>
                                <tr>
                                    <th>{{ __('Confirmed', [], $locale.'-plus') }}:</th>
                                    <td class="text-break">
                                        @if ($email_verified_at != null)
                                            {{ $email_verified_at }}
                                        @else
                                            {{ __('Confirmed', [], $locale.'-plus') }} 
                                            <button class="profile-edit-btn" type="submit" form="email-resend">{{ __('Resend', [], $locale.'-plus') }}</button>
                                        @endif
                                    </td>
                                </tr>
                                <tr>
                                    <th>{{ __('Registration', [], $locale.'-plus') }}:</th>
                                    <td>{{ $user->created_at }}</td>
                                </tr>
                                <tr>
                                    <th class="border-bottom-0">{{ __('Profile picture', [], $locale.'-plus') }}:</th>
                                    <td class="border-bottom-0 my-input-file my-text-ellipsis"><input class="profile-edit-btn first-line-hide" id="avatar" type="file" name="avatar" accept=".png, .jpg, .jpeg" onchange="InputTextChange();"><span id="input-text" class="none">@if($user->avatar=='default.jpg'){{ __('Default', [], $locale.'-plus') }}@else{{ __('Custom', [], $locale.'-plus') }}@endif</span></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="row mt-3 p-2 ms-1 me-1 text-center">
                    <div class="col pe-1">
                        <a class="btn btn-danger my-btn-danger container-fluid p-2" href="/profile/{{ $user->username }}">{{ __('Back', [], $locale.'-plus') }}</a>
                    </div>
                    <div class="col ps-1">
                        <button class="btn btn-success my-btn-success container-fluid p-2" type="submit">{{ __('Save', [], $locale.'-plus') }}</button>
                    </div>
                </div>
            </form>
            @if($errors->any())
                <div class="container pb-2 mt-2 my-bg-red-gradient rounded">
                    <div class="text-center mt-2">
                        @error('username')
                            {{ $message }}
                        @enderror
                        @error('avatar')
                            {{ $message }}
                        @enderror
                    </div>
                </div>
            @elseif (isset($message))
                <div class="pb-2 mt-2 mx-1 my-bg-green-gradient rounded">
                    <div class="text-center mt-2">
                        <p class="mb-0">{{ $message }}</p>
                    </div>
                </div>
            @endif
        </div>

@endsection