<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\PageController;
use App\Http\Controllers\PostController;
use Illuminate\Foundation\Auth\EmailVerificationRequest;
use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('/home', [
        'page' => '/'
    ]);
});

Route::get('/scoreboard', [PageController::class, 'Scoreboard']);
Route::get('/about', function () {
    return view('/about', [
        'page' => '/about'
    ]);
});

Route::post('/login', [PostController::class, 'Login']);
Route::get('/login', function () {
    return view('/login', [
        'page' => '/login'
    ]);
})->name("login");

Route::post('/registration', [PostController::class, 'Registration']);
Route::get('/registration', function () {
    return view('/registration', [
        'page' => '/login'
    ]);
});

Route::get('/profile/{username}', [PageController::class, 'Profile']);

Route::get('/logout', [PageController::class, 'Logout']);

Route::get('/search', [PageController::class, 'Search']);

Route::post('/delete', [PostController::class, 'Delete']);
Route::get('/delete', [PageController::class, 'Home']);

Route::post('/delete-auth', [PostController::class, 'DeleteAuth']);
Route::get('/delete-auth', function () {
    return view('/delete-auth', [
        'page' => '/profile',
        'my' => true
    ]);
});

Route::post('/forgot-password', [PostController::class, 'ForgotPassword']);
Route::get('/forgot-password', function () {
    return view('/forgot-password', [
        'prev_url' => url()->previous(),
        'message' => session()->get('message') != null ? session()->get('message') : null,
    ]);
});

Route::post('/reset-password', [PostController::class, 'ResetPassword']);
Route::get('/reset-password/{token}', function (string $token) {
    return view('reset-password', [
        'token' => $token
    ]);
})->name('password.reset');

Route::post('/change-password', [PostController::class, 'ChangePassword']);
Route::get('/change-password', function () {
    return view('/change-password', [
        'page' => '/profile',
        'my' => true
    ]);
});

Route::get('/edit', [PageController::class, 'Edit']);
Route::post('/edit', [PostController::class, 'Save']);

Route::get('/email/verify', function () {
    return view('auth.verify-email');
})->middleware('auth')->name('verification.notice');

Route::get('/email/verify/{id}/{hash}', function (EmailVerificationRequest $request) {
    $request->fulfill();
 
    return view('/success', [
        'page' => '/',
        'title' => 'Az e-mail visszaigazolva.',
        'buttonHref' => '/',
        'buttonText' => 'Ok'
    ]);
})->middleware(['auth', 'signed'])->name('verification.verify');

Route::post('/email/verification-notification', function (Request $request) {
    $request->user()->sendEmailVerificationNotification();
 
    return back()->with('message', 'Az e-mail megerösitő link elküldve!');
})->middleware(['auth', 'throttle:6,1'])->name('verification.send');

Route::get('/change-language/{locale}', [PageController::class, 'ChangeLanguage']);
