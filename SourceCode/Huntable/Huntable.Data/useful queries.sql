select ReferralId,count(Id) from [user] 
where IsPremiumAccount = 1
group by ReferralId
having ReferralId is not null
