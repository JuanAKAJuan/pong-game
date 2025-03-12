# pong-game

### TODO:
- [x] Generate a sprite font for the scores and add it to the Content Pipeline. The recommended font size is 70,
but you can certainly use other font sizes. Use Bold style. Set the maximum score to be 100.
- [x] Replace the ball texture with a ball image created by yourself or downloaded from Internet. You may need
to convert the image format from .gif to .png, since .gif is not supported by XNA.
- [x] Add a background image (texture) to the game. The background can simulate the table tennis table or other things.
- [x] Add sound effects when the ball is bounced by the paddle or when the ball hits the left or right border.
Some sound effects can be found at this website: http://www.pacdv.com/sounds/interface_sounds.html. Use the SoundEffect class for this task.
- [x] Add background music (using .wma or .mp3 formats) to the game. Set the play mode to repeat so that the background music
is continuously played. Use the MediaPlayer and Song classes for this task.
- [x] Handle the following keyboard inputs.
    - Reset the game using "R".
    - Exit the game using "Q" or "ESC".
    - Toggle (pause and resume) the background music using "P".
- [x] Randomize the initial speed of the ball in the Game1. ResetGame() method so that the x component of the
speed is in the intervals [4, 6] or [-6, -4] and the y component is in the intervals [3, 5] or [-5, -3]. Use the Random class for this task.
- [x] Display a text that displays your name. You need to generate a new sprite font.
