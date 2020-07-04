# comment-system-blog
This repository contains commenting system for my [blog](https://blogs.roarcoder.dev)

## Architecture
<p align="center">
  <img src="/assets/images/architecture.png">
</p>

## Action Plan
1.  Create an HTML5 form in each post
2.  Send the user's comment to API Gateway using AJAX POST method
3.  API Gatway triggers lambda (written in C#) which store the comment in DynamoDB
