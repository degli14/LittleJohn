LittleJohn - Fictitious Brokerage Service

This exercise is a small HTTP api to read a fictitious brokerage service. It's a self-hosted WebAPI that should host the functionalities on localhost port 8080 once launched.

LittleJohn exposes three API calls:

 - /users/register/{name}		-> where {name} is a string username: this method will return a token that you have to use as basic auth for next calls (psw must be empty)
 - /tickers						-> when requested with basic auth will return your Portfolio with prices of the current day
 - /tickers/{tickerId}/history	-> when requested with basic auth will return the evolution of the price of the ticker with id {tickerId} for the last 90 days (current day included)

Supported tickers -> AAPL, MSFT, GOOG, AMZN, FB, TSLA, NVDA, JPM, BABA, JNJ, WMT, PG, PYPL, DIS, ADBE, PFE, V, MA, CRM, NFLX

This WebApi uses persistence only when a user is registered. The rest is procedural.