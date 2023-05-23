@extends('layout')
@section('content')

        <div class="container pb-3">
            <h1 class="text-center display-6 py-3">{{ __('About', [], $locale.'-plus') }}</h1>
            <p class="fs-5">
                Ez a projekt egy szakdolgozathoz készült.
            </p>
            <p>Fejlesztők:</p>
            <p>Mikes dávid: Készitette a játékot.</p>
            <p>Dömök Dávid: Készitette a weboldalt.</p>
        </div>

@endsection