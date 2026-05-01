# Shopping Cart System (C#)
This is my quiz requirement (quiz 2 and 3) for Computer Programming 2, before Midterms.
This project is a console-based shopping cart system with stock mananagement.
- UPDATE: This is the part 2 of the said quiz. A ennhanced version of the previous cart system.
A console-based shopping cart system added with cart management and payment system.


## Features (Part 1)
- Product Selection
- Stock Validation
- Cart System
- Discount Logic

## Features (Part 2)
- Cart Management Menu
- Product Search
- Product Categories
- Stock Reorder Alert
- Checkout Payment Validation
- Updated receipt with date and number
- Order History
- Enhanced Continue Prompt Validation

## AI Usage in this Project (Part 2)
What part of the project did i use AI?
- In doing a pull request and creating a new branch.
- As usual, i asked AI for a workflow to follow through in this project.
- Mainly used for debugging, when i converted my List<> of cart items in a fixed-sized array i encounter errors such as, the arguments needed in specific methods, for and foreach loops, and computation of the prices.
- In adding the search feature i encounter a problem, I'm stucked in a decision whether to remove the display of products in the beginning so that the search feature would be fully utilized, but if i remove it the users would have to guess the products that are available. So i asked AI for solutions. At the end, i just left untouched the display of products and added the search feature as an option to the menu.
- In the start i have two while loops in main(), outer loop for shopping and inner loop for the cart. At first it was okay but when i started to add the other features i realized that this kind of setup is buggy, messy, and prone to errors. I used AI to this part and it suggested that i integrated the two in just one loop and so i did it. The original store menu display was turned into a method and the main() codes are much less now compare to before.

## Why i used AI in those parts?
- I used AI especially on git and github because those parts are more technical and I'm not confident in just doing certain things without proper guidance. One mistake in this part would lead to more problems later so that's why i used AI.
- In the workflow part. I'm more productive and whenever there is a plan to work through the project becomes much easier and manageable.
- In debugging. Same as the git part, although i know how to fix some of the broken code there's still a part for me that is quite confusing and advanced; this part is also technical as well.
- Due to limited time, i used AI in helping me make decisions that are efficient and to save time.
- Usually the part where "i clean my output display" would be included in this but this time around i focused much more on the logic and syntax itself. I also take time in analyzing and understanding the code may it be suggested by AI or created by me.

## What prompts/questions i asked?
- (Attachment:Quiz-part-2.pdf) "As usual, make me a workflow for this project. The deadline is on Friday at 12 noon. Also help me adress the comments about my previous project: {the comments in the gdrive.....}.
- "In doing the PR part of this project, how can i create a new branch then make a pull request in github?"
- "(Attachment: Program.cs) I've changed my original cart into a fixed sized cart array. Help me debugged the errors caused by this conversion."
- "what's more efficient? integrating the search option in the cart menu (when runnning the program, the store menu display all of the products and prompt the user to add product number and etc before going to the cart menu, i was thinking that after running the program it will just jump straight to cart menu and the search feature as an option would be there instead) or that recommendation that you provided, because it baffles me, like what is the point of a search feature if the all the product is going to be displayed anyway and if the user would be allowed to search first then buy or vice versa? (in answering this, based the answer on the needed requirement on the pdf i've sent you)"
- "(Attachment: Program.cs) [this is where i encounter the problem of having a nested loop in main()} now i'm stuck after adding search feature, check my code and suggest solutions for it?"

## What i've changed or improved after using AI?
- My time management
- The code structrure specially in main() are much cleaner now compare to before.
- The efficiency improve and my productivity as well. It feels like i have a buddy besides me guiding me throughout this project.
- My understanding in the usage of methods in c#.
- Lastly, I've changed some of the output display as well.

## Author
Diaz, Daren V.
BSIT 1-2
