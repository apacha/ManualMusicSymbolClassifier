# Manual Music Symbol Classifier

This repository contains a small WPF application that is used for manually classifying images into one of 32 different categories.
It is part of a set of tools:

|[Model Trainer](https://github.com/apacha/MusicSymbolClassifier)|[Mobile App](https://github.com/apacha/MobileMusicSymbolClassifier)|[Manual Classifier](https://github.com/apacha/ManualMusicSymbolClassifier)|
|:----:|:-----:|:-----:|
|Trains a deep network to automatically classify images of handwritten music symbols into 32 different classes.|Mobile Android application that uses a trained model to perform real-time classification on a mobile device.|A small C#/WPF application that can be used manually classify images, used during evaluation|
|[![Build Status](https://travis-ci.org/apacha/MusicSymbolClassifier.svg?branch=master)](https://travis-ci.org/apacha/MusicSymbolClassifier)|TBD|[![Build status](https://ci.appveyor.com/api/projects/status/2lxb6eg6qnfj9jq5?svg=true)](https://ci.appveyor.com/project/apacha/manualmusicsymbolclassifier)|
|[![codecov](https://codecov.io/gh/apacha/MusicSymbolClassifier/branch/master/graph/badge.svg)](https://codecov.io/gh/apacha/MusicSymbolClassifier)|||

Note my previous project which classifies images into Music scores or something else which can be found in [this](https://github.com/apacha/MusicScoreClassifier) repository on Github.

# Building the application
This application is a C#/Wpf application that requires Visual Studio to build.

## Authors
Alexander Pacha, TU Wien

## License

Published under MIT License,

Copyright (c) 2017 Alexander Pacha

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.