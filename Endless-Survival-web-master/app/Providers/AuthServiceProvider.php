<?php

namespace App\Providers;

// use Illuminate\Support\Facades\Gate;
use Illuminate\Foundation\Support\Providers\AuthServiceProvider as ServiceProvider;
use Illuminate\Auth\Notifications\VerifyEmail;
use Illuminate\Notifications\Messages\MailMessage;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\URL;
use App\Models\User;

class AuthServiceProvider extends ServiceProvider
{
    /**
     * The model to policy mappings for the application.
     *
     * @var array<class-string, class-string>
     */
    protected $policies = [
        // 'App\Models\Model' => 'App\Policies\ModelPolicy',
    ];

    /**
     * Register any authentication / authorization services.
     *
     * @return void
     */
    public function boot()
    {
        $this->registerPolicies();

        
        VerifyEmail::toMailUsing(function (object $notifiable, string $url) {
            return (new MailMessage)
                ->greeting('Hello')
                ->subject('E-mail cím visszaigazolás')
                ->line('Sikeresen regisztráltál a szakdgame oldalra. Kérjük igazold vissza az e-mail címed')
                ->line('Az alábbi gombra kattintva igazolhatod az e-mail címed.')
                ->action('E-mail cím visszaigazolása', $url)
                ->line('Ha nem te regisztráltál figyelmen kivül hagyhatod az üzenetet.');
        });
    }
}
