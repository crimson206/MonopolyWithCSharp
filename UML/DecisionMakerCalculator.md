# 1. Factors
## 1. MonopolyFactor

The size of a group of a property \
$\quad {Property.Group}_{cnt, tot}$ \
\
The members count of a group of property owned by a player \
$\quad{Property.Group}_{cnt, player}$ 

Example \
$\quad RealEstates \ "Red1", "Red2", "Red3" \in Red(k).Group$ \
$\quad Player1 \ owns \ "Red1" and "Red2"$ \
$\quad {Red(k).Group}_{cnt, tot} = 3 \quad \forall \ k $ \
$\quad {Red1.Group}_{cnt.p1} = {Red1.Group}_{cnt.p1} = 2$ 

Monopoly factor \
$\quad {F}_{monopoly}(Property, Player) \equiv { {Property.Group}_{cnt,tot} + 1 \over {Property.Group}_{cnt,tot}-{Property.Group}_{cnt,player} + 1} $

$\quad {F}_{monopoly, getting}(Property, Player) \equiv { {Property.Group}_{cnt,tot} \over {Property.Group}_{cnt,tot}-{Property.Group}_{cnt,player}} $

If Player1 owns every red realestate, \
$\quad {F}_{monopoly}(Red1, P1) = { 3 + 1 \over 3-3 + 1} = 4 $


Summary : the factor is larger as the group is bigger and is closer to monopoly. 



## 2. FreePropertyFactor
$\quad {F}_{free.property} \equiv {{Properties}_{cnt, free} \over {Properties}_{cnt, tot}}$

## 3. PriceFactor
$\quad PriceRank(Property) \equiv { Property.Price \ - \ Min(Price) \over Max(Price) \ - \ Min(Price)}$ \
$\quad {F}_{price}(Property) \equiv 1 \ + {PriceRank(Property) - 0.4 \over 3} $ \
Summary : the more expensive it is, the better it is.

## 4. RestBalancePerRentsFactor
$\quad {F}_{bal/rents}(Player , Cost) \equiv $
$\quad { Player.Balance \over Player.Balance \ + \ Cost \ + \ \Sigma(Property.CurrentRent)} ,\ \forall \ Property \ such\ that \ Property.Owner \ne null \ nor \ Player$ 

Summany : If the property is expensive or enemies' rents sum is high, the player is unwilling to pay more; However, if the player's balance is high enough, they are ignored.


# 2. PropertyValue
value property more if it is easier to monopoly and more expensive \
$ a = {F}_{monopoly}(Property, player) *{F}_{price}(Property) $ \
\
if there are many free properties, players are not willing to pay more \
$ b(a) = 1 + (a - 1) * (1- {F}_{free,property}) $ 

revalue it according to the economic situation \
$ c(b) = b \ * \ {F}_{bal/rents}(Player , Cost) $

${V}_{getting}(Property,Player,Cost) = c(b({a}_{getting})) = c(Property,Player,Cost)$

$V(Property,Player) = b({a}) = b(Property,Player)$

$a$ is usually larger than 1 \
$b \ and \ c$ are always smaller than 1

# 3. DecisionMaker
## 1. BuyProperty
A player buy a property if \
${V}_{getting}(Property,Player,Cost) \ge 1$

## 2. BidAuction
A player raise 20$ if \
$Vgetting(Property, Player, CurrentAuctionPrice + 20 ) \ge 1 $

, it is programmed differently. Refactor first and continue writing this.