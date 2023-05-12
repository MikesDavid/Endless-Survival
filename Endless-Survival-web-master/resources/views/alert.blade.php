@extends('layout')
@section('content')

        <div class="container px-0">
            <div class="pb-3 mt-2 mx-1 my-bg-red-gradient rounded">
                <h1 class="text-center display-6 py-3">{{ $title }}</h1>
                <div class="text-center">
                    <form action="/{{ $yes }}" method="post">
                        @csrf
                        <button class="btn btn-danger my-btn-danger ms-auto" type="submit">{{ __('Yes', [], $locale.'-plus') }}</button>
                        {{-- <a class="btn btn-danger my-btn-danger ms-auto" href="/{{ $yes }}">Igen</a> --}}
                        <a class="btn btn-success my-btn-success ms-auto" href="/{{ $no }}">{{ __('No', [], $locale.'-plus') }}</a>
                    </form>
                </div>
            </div>
        </div>

@endsection