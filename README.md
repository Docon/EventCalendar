EventCalendar
=============

A ASP.NET C#/JQuery/AJAX event calendar using Google Calendar API that can be dropped into web pages. 

Extends Calendario (an open source JQuery calendar plugin) - https://github.com/codrops/Calendario

Uses the Google Calendar API for C# ASP.NET  - https://developers.google.com/google-apps/calendar/

This code was originally developed for an Introduction to Web Programming class to show examples of web services, a data provider using a popular API, and AJAX web service calls from JQuery to retrieve and format JSON data using C#. This code has no license except for the the licenses of the Google Calendar API and Calendario. I have adapted Calendario to only load one month of events at a time using a Jquery AJAX call. This project is free to use and modify. To make this code production-ready one should probably add logging, unit tests, and optimize for performance - maybe cache various timeframes such as this month and next month.

Configuration for the Google Calendar account can be set in AppConfig/EventCalendar.config