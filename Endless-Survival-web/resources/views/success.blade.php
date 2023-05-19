@extends('layout')
@section('content')

        <div class="container px-0">
            <div class="container pb-3 mt-2 mx-1 my-bg-green-gradient rounded">
                <h1 class="text-center display-6 py-3">{{ $title }}</h1>
                <div class="text-center">
                    <a class="btn btn-dark ms-auto" href="{{ $buttonHref }}">{{ $buttonText }}</a>
                </div>
            </div>
        </div>

@endsection