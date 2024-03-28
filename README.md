# ISS_Buying_Selling
We are creating the buying and selling part for groups for a social media platform



Our tasks :

take each big task and make it into 4 roughly equal parts 

Don t forget - feasible, valid, unambiguous, verifiable, consistent 

If we don t give him reasons, he will give us all the money. 

If something is dependent on other groups tasks, assume it has a callable function that gets the info.

----WE GOT BUYING/SELLING----


They are features for already coded(by someone else) groups.



Make a marketplace inside a group

A group admin can give a user the ability to sell something
A user can request a sell
Admins have the ability to remove a post
Users can report a post
At a certain number of reports, a users right to sell is withdrawn (over 50% of users who have seen it called it a scam, minimum 10 views)


- add tags
- add reviews for selling and buying
- ban users from selling or buying stuff
- allow sellers to not allow certain users to buy

Two options for selling: 
- a fixed price
- auction, ends when the seller decides, minimum 1 day, max 1 month(last 30 seconds, for each bid, the timer resets to 30 seconds)
- seller can decide to end an auction prematurely if they are happy with the bid, they can also cancel
- seller can set automatic end for auction when certain price is hit
- seller can set starting price, if they want
- donations are allowed - the minimum price == 0; 
- buy now feature, if the seller confirms that they agree to end the auction and sell at that price
- display a product inside of an event at which you can buy it
			  
		
Vanzare 

-> auction 
-> fixed price 
-> f2f

Target complexity ~ 1 database/table per team member 

Entities:
- groups
- users
- reports
- posts
- groups




All selling Posts:
- any user who isn't banned ca make a request to the administrators to sell a product at a fixed price
- any admin or moderator can validate or reject a request from a user to sell a product
- any user can report the post
- when a selling post has received more than 50% of reports from people that have seen the post(more than 10 people), the post will be sent to a moderator to validate
- any user has a rating for buying and a different rating for selling
- any user with a selling rating over 75% does not need to make a request to the administrators to sell something
- any user without a selling rating or a selling rating below 75% will need to make a request to an administrator to sell a product
- any users who makes a selling post can choose a minimum buying rating for the users that can buy the product
- when a transaction ends the buyer and the seller choose a rating for each other and have the option of writing a review
- any user can add tags to their selling post, maximum of 10 tags

Fixed price:
- selling with a fixed price decided the by the seller
- a fixed price post will be deleted if nobody buys it in 3 months
- a buy is confirmed by the seller, it can choose if the transaction goes forward or not
- a buy is confirmed after by the buyer
Donations:
- any user can make a request to the moderators to be able to request donations
- any user can donate
- users with a rating over 75% do not need to make requests to the moderators to request donations

Auctions:
- a user can create an auction by either making a request to the moderators or having a rating over 75%
- the user who creates the auction(A) can set a minimum bidding price
- A can set the starting price
- A can decide when to end the auction, either by ending it manually or choosing a price at which the auction will end
- A can also end the auction by agreeing with a buyer for a certain price
- any auction must last at minimum 1 day, and at maximum 30 days
- if the auction has less than 30 seconds left, any bid made will reset the time until it expires to 30 seconds

Donation:
- a user can create a donation post for any cause
- the donation post must have a button which, when pressed, will open the website of the charity	
	
	
	
