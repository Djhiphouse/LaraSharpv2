#!/bin/bash

# Install Mono if not already installed
if ! command -v mono &> /dev/null
then
    echo "Mono is not installed. Installing via Homebrew..."
    brew install mono
fi

# Download nuget.exe if not already downloaded
if [ ! -f "nuget.exe" ]; then
    echo "Downloading nuget.exe..."
    curl -o nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
fi

# Restore NuGet packages
echo "Restoring NuGet packages..."
mono nuget.exe restore

# Build the project
echo "Building the project..."
msbuild /p:Configuration=Release

echo "Build completed."

