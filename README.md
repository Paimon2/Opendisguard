# Opendisguard
Opendisguard is a self-hostable Discord bot which combats spam using CAPTCHAs locally. (WIP)

It differs from other bots by:
- Having completely local CAPTCHA generation, no dependency on reCAPTCHA/hCaptcha/2Captcha
- Being easily modifable to generate more complex CAPTCHA images (as OpenCV is used)
- Being scalable to multiple servers and users
- Being free and open source!

### Technologies used
- OpenCV/OpenCvSharp *for generating captcha images*
- Discord.Net *for making the bot work!*
- SQLite *for storing data such as verification codes and server-specific settings*

### Usage

1. Create a file named *"token.txt"* in the directory of the Opendisguard executable. 
2. Paste the token of your bot into this file.
3. TODO
