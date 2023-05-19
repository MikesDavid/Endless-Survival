<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;
use Illuminate\Support\Facades\Auth;
use App\Models\User;
use Illuminate\Support\Facades\Hash;
use Illuminate\Auth\Events\Registered;
use Illuminate\Support\Facades\Session;
use Illuminate\Support\Facades\Password;
use Illuminate\Auth\Events\PasswordReset;
use Illuminate\Support\Str;
use Carbon\Carbon;

class PostController extends Controller
{
    function Registration(Request $request){
        $request -> validate([
            'email' => 'required|max:100|email|unique:users,email',
            'username' => 'required|max:15|unique:users,username',
            'password' => 'required|max:50|min:8',
            'password_confirmation' => 'required|same:password'
        ], [
            'email' => __('validation.required', ['attribute' => __('validation.attributes.email', [], app()->getLocale().'-plus').' vagy '.__('validation.attributes.username')])
        ]);

        $user = new User();
        $user -> email = $request -> email;
        $user -> username = $request -> username;
        $user -> password = Hash::make($request -> password);
        $user -> Save();

        event(new Registered($user));

        return view('/success', [
            'page' => '/login',
            'title' => __('You have successfully registered!', [], app()->getLocale().'-plus'),
            'buttonHref' => '/login',
            'buttonText' => __('Login', [], app()->getLocale())
        ]);
    }
    
    function Login(Request $request){
        $request->validate([
            'username' => 'required',
            'password' => 'required'
        ], [
            'username.required' => __('validation.required', ['attribute' => __('validation.attributes.email', [], app()->getLocale().'-plus').' vagy '.__('validation.attributes.username')])
        ]);

        $email = Validator::make($request->all(), [
            'username' => 'email'
        ]);

        if (!($email->fails()))
            $field = 'email';
        else
            $field = 'username';

        if (Auth::attempt([
                    $field => $request->username, 
                    'password' => $request->password, 
                    ],
                $request->remember
            ))
        {
            $resolution = getimagesize('img/avatar/'.Auth::user() -> avatar);
            session() -> put('blur_off', $resolution[0] < 256 || $resolution[1] < 256);
            
            return redirect('/');
        }
        else
        {
            return view('/login', ['error' => __('Incorrect username or password', [], app()->getLocale().'-plus')]);
        }
    }

    function ForgotPassword(Request $request){
        $request->validate([
            'username' => 'required'
        ], [
            'username.required' => __('validation.required', ['attribute' => __('validation.attributes.email').' vagy '.__('validation.attributes.username')])
        ]);

        $email = Validator::make($request->all(), [
            'username' => 'email'
        ]);

        if (!($email->fails()))
            $field = 'email';
        else
            $field = 'username';
        
        $status = Password::sendResetLink([$field => $request->username]);

        return redirect('/forgot-password')->with(['message' => __($status)]);
    }

    function ResetPassword(Request $request){
        $request->validate([
            'password' => 'required|max:50|min:8',
            'password_confirmation' => 'required|same:password'
        ]);
        
        $status = Password::reset(
            $request->only('email', 'password', 'password_confirmation', 'token'),
            function (User $user, string $password) {
                $user->forceFill([
                    'password' => Hash::make($password)
                ])->setRememberToken(Str::random(60));
                
                $user->save();
                
                event(new PasswordReset($user));
            }
        );

        return view('/success', [
            'page' => '/',
            'title' => __($status),
            'buttonHref' => '/',
            'buttonText' => __('Ok', [], app()->getLocale().'-plus')
        ]);
    }

    function ChangePassword(Request $request){
        $locale = app()->getLocale();
        $request->validate([
            'password' => 'required',
            'new_password' => 'required|max:50|min:8',
            'new_password_confirmation' => 'required|same:new_password'
        ], [
            'new_password.required' => __('validation.required', ['attribute' => __('validation.attributes.new_password', [], $locale.'-plus')]),
            'new_password.max' => __('validation.max', ['attribute' => __('validation.attributes.new_password', [], $locale.'-plus')]),
            'new_password.min' => __('validation.min', ['attribute' => __('validation.attributes.new_password', [], $locale.'-plus')]),
            'new_password_confirmation.required' => __('validation.required', ['attribute' => __('validation.attributes.new_password_confirmation', [], $locale.'-plus')]),
            'new_password_confirmation.same' => __('validation.same', ['attribute' => __('validation.attributes.new_password_confirmation', [], $locale.'-plus')])
        ]);

        if (Hash::check($request -> password, Auth::user()->password)){
            $user = Auth::user();
            $user -> password = Hash::make($request -> new_password);
            $user -> Save();

            return view('/success', [
                'page' => '/profile',
                'title' => __('You have successfully changed your password.', [], app()->getLocale().'-plus'),
                'buttonHref' => '/profile/'.Auth::user()->username,
                'buttonText' => __('Ok', [], app()->getLocale().'-plus')
            ]);
        }
        else
            return view('/change-password', [
                'page' => '/profile',
                'my' => true,
                'error' => __('Wrong password.', [], app()->getLocale().'-plus')
            ]);
    }

    function Save(Request $request){
        $user = User::find(Auth::user()->id);
        $change = false;

        if ($request->username != $user->username)
        {
            $request->validate([
                'username' => 'required|max:15|unique:users,username'
            ]);
            $change = true;

            $user -> username = $request -> username;
            $user -> save();
        }

        $avatar = Validator::make($request->all(), [
            'avatar' => 'required'
        ]);

        if (!($avatar->fails()))
        {
            $request->validate([
                'avatar' => 'image|mimes:jpeg,png,jpg|max:2048'
            ]);
            $change = true;
        
            if($user -> avatar != 'default.jpg' && file_exists(public_path('img/avatar/'.$user -> avatar)))
                unlink(public_path('img/avatar/'.$user -> avatar));

            $avatar = $user->id.'.'.$request->avatar->getClientOriginalExtension();
            $request->avatar->move(public_path('img/avatar'), $avatar);

            $user -> avatar = $avatar;
            $user -> save();
            
            $resolution = getimagesize('img/avatar/'.$user -> avatar);
            session() -> put('blur_off', $resolution[0] < 256 || $resolution[1] < 256);
        }

        return view('/edit', [
            'page' => '/profile',
            'my' => true,
            'user' => $user,
            'message' => $change ? 'A módosítások sikeresen el lettek mentve.' : null,
            'blur_off' => session()->get('blur_off'),
            'email_verified_at' => $user->email_verified_at != null ? Carbon::parse($user->email_verified_at)->format('Y-m-d H:i:s') : null
        ]);
    }

    function DeleteAuth(Request $request){
        $request->validate([
            'password' => 'required'
        ]);

        if (Hash::check($request -> password, Auth::user()->password))
            return view('/alert', [
                'page' => '/profile',
                'my' => true,
                'title' => __('Are you sure you want to permanently delete your account?', [], app()->getLocale().'-plus'),
                'yes' => 'delete',
                'no' => 'profile/'.Auth::user()->username
            ]);
        else
            return view('/delete-auth', [
                'page' => '/profile',
                'my' => true,
                'error' => __('Wrong password.', [], app()->getLocale().'-plus')
            ]);
    }

    function Delete(){
        $user = User::find(Auth::user()->id);
        $user -> delete();

        Session::flush();

        return redirect('/');
    }
}
