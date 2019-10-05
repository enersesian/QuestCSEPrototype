# QuestCSEPrototype
Oculus Quest prototype for a computer science educational experience. Current level is introduction lesson on binary math for middle school and high school students. Next step is a low fidelity prototype of the next level, which is bubble sort. User testing for buble sort prototype is November 22nd, 2019. Bubble sort learning mechanics should work well with the physical movements that our interactions are designed around. Developer blog: http://artncoding.com/vrcsapp-blog Design doc: http://artncoding.com/vrcsapp-design-doc

5/28/19 - Completed first prototype iteration<br/>
6/08/19 - Completed user testing on 50 middle school students<br/>
6/14/19 - Learning assessments confirm VR experience is as effective as an instructor led lesson following csunplugged.org<br/>
6/28/19 - Finish refining core interactions, UX/UI, and learning goals before moving on to graphics, narrative and audio<br/>
9/24/19 - Completed refinement of binary math level and presented demo at Oculus Connect 6 Educational Summit<br/>
10/5/19 - Finished cleaning scripts after OC6 demo<br/>

000_OC6Demo.unity is setup to play on Oculus Rift in editor and builds to Oculus Quest. Both use touch controllers, and has standard grab input on levers and grabbables. Level is 20'x20'. For Rift use in editor, use thumbpad on right or left controller, defaults to right, to move around scene. Press all four buttons, A B X Y, to reset station height mid-game. Station height gets set multiple times at beginning based on user height but may need to be reset mid-game. 

Next refactorings:
Change status and requirements of current task from int array to enumerations.
Create a base station script for inheritence, and make station scripts singletons like the UserManager and LevelManager.
Change public variables on stations to private serialized fields
Change physics on the carrying container to stop it from vibrating while carrying it to task station

PROGRAMMING CREDITS<br/>
Programmer: Eric Nersesian<br/>
Data Analytics Programmer: Shannon Hargrave<br/>
DESIGN CREDITS<br/>
UX Designers: Jessica Ross, Eric Nersesian, Dr. Margarita Vinnikov, Adam Spryszynski<br/>
Visual and Narrative Development: Jessica Ross<br/>
Graphic Design: Jerry Bellone<br/>
3D Modeling: Cris Guzman, Todd Seeling, Brent Hartwell<br/>
Texturing: Shannon Hargrave<br/>
Level Design: Eric Nersesian<br/>
RESEARCH CREDITS<br/>
VR UX Researchers: Dr. Margarita Vinnikov, Dr. Michael Lee, Eric Nersesian, Adam Spryszynski<br/>
CS Education Researchers: Dr. Michael Lee, Dr. Margarita Vinnikov, Eric Nersesian, Adam Spryszynski<br/>
Research Assistants: Namita Mahindru, Adam Spryszynski
