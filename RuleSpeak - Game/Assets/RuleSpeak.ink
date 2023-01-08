VAR attack = 0
VAR winchance = 1
VAR roundCount = 0

VAR Ycounter = 0
VAR Bcounter = 0

VAR 5syl = ""
VAR 7syl = ""


VAR rounds_won = 0
VAR rounds_lost = 0

VAR won = false

VAR ybmode = false

VAR Counter = 0


Wäs willscht dü spielön?

+ Einfacher Modus #speaker:Flinta #state:loop
    -> ATTACK_HANDLER 
 
 + Schwerer Modus #speaker:Flinta #state:loop
    ~ybmode = true
    -> ATTACK_HANDLER 
    

=== ATTACK_HANDLER ===
{Ycounter == 3 && Bcounter == 5: {Yreset()}} 
~roundCount +=1
//~winchance +=1
{YBModeA()}

{roundCount:
- 4: ~roundCount = 1
}


{attack0()}

//says AI-Attack in the Dialogue-Output
{attack: 
- 1: Spiegel 'asson disch! #speaker:Regina
- 2: Die Schu'e stinkon! #speaker:Regina
- 3: Disch polier isch noch!#speaker:Regina
- 4: Et voilà la merde!#speaker:Regina
- 5: Du Leischtmatrosa!#speaker:Regina
- 6: Davy Jones' Kiste wartät! #speaker:Regina
- 7: Dein 'olzbein wird varfauart! #speaker:Regina
- 8: Du bischt ein raudigar 'und! #speaker:Regina
- 9: ubar die Planka mit dir! #speaker:Regina
- 10: Tu as un sacré culot! #speaker:Regina
- 11: Yo-'o-'o #speaker:Regina 
- 12: Yo-'o-'o #speaker:Regina  
- 13: Yo-'o-'o #speaker:Regina 
- 14: Yo-'o-'o #speaker:Regina 
- 15: Yo-'o-'o #speaker:Regina 
- 16: und na Budal voll Rum #speaker:Regina
- 17: und na Budal voll Rum #speaker:Regina
- 18: und na Budal voll Rum #speaker:Regina
- 19: und na Budal voll Rum #speaker:Regina
- 20: und na Budal voll Rum #speaker:Regina
}

//checks if AI-Attack is correct and acts accordingly 
{Ycounter:
-3: {Ycheck()}
}
{Bcounter:
-5: {Bcheck()}
}
{Ycounter != 3 && Bcounter !=5:{Ncheck()}}
{won == true: -> CHOICES| -> ROUND_WON}






=== CHOICES ===
//chooses a random Attack for the 5syl and 7syl button


{ybmode == true: {YBModeB()}}
~roundCount += 1
{Ycounter == 4:     {Counter1(Ycounter)}}
{Bcounter == 6:     {Counter1(Bcounter)}}
{roundCount == 4:   {Counter1(roundCount)}}


~5syl = RANDOM(1,5)
~7syl = RANDOM(1,5)

+ {5syl == 1} [5 Silben]
    Beim Klabautermann! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ROUND_LOST
    - else: -> ATTACK_HANDLER
    }
    
+ {5syl == 2} [5 Silben]
    Halt's Schandmaul, Sprotte! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ROUND_LOST
    - else: -> ATTACK_HANDLER
    }
    
+ {5syl == 3} [5 Silben]
    Versteck dich lieber! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ROUND_LOST
    - else: -> ATTACK_HANDLER
    }
    
+ {5syl == 4} [5 Silben]
    Verflucht sei dein Blut! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ROUND_LOST
    - else: -> ATTACK_HANDLER
    }
    
+ {5syl == 5} [5 Silben]
    Das Vieh gehort hinaus! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ROUND_LOST
    - else: -> ATTACK_HANDLER
    }
    
    
+ {7syl == 1} [7 Silben]
    Nimm den Stiefel aus dem Maul! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ATTACK_HANDLER
    - else: -> ROUND_LOST
    }
    
+ {7syl == 2} [7 Silben]
    Pockiger Bilgenaffe! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ATTACK_HANDLER
    - else: -> ROUND_LOST
    }  
    
+ {7syl == 3} [7 Silben]
    Dich werd ich Kiel holn lassen! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ATTACK_HANDLER
    - else: -> ROUND_LOST
    }
    
+ {7syl == 4} [7 Silben]
    Bist den Kugeln zu schade! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ATTACK_HANDLER
    - else: -> ROUND_LOST
    }
    
+ {7syl == 5} [7 Silben]
    Dei Visage schreckt nicht schlecht! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter == 5: -> ROUND_LOST}
    {roundCount:
    -2: -> ATTACK_HANDLER
    - else: -> ROUND_LOST
    }   
    
+ [Yo-ho-ho!]
    Yo-ho-ho! #speaker:Flinta
    {Ycounter:
    -3: -> ATTACK_HANDLER
    }
    {Bcounter == 5: -> ROUND_LOST | -> ROUND_LOST}  

    
+ [Und ne Budel voll Rum!]
    Und ne Budel voll Rum! #speaker:Flinta
    {Ycounter == 3: -> ROUND_LOST}
    {Bcounter:
    -5:     -> ATTACK_HANDLER 
    -else:  -> ROUND_LOST
    }
    
    
+ Zu langsam! #speaker:Regina
    -> ROUND_LOST
 
 
=== ROUND_LOST ===

Ah oui, du musst trinkon! #speaker:Regina

{rounds_lost == 5: -> STOP}

~rounds_lost += 1
~Ycounter = 0
~Bcounter = 0
~roundCount = 0
~Counter = 0

Also, von vorn.



-> ATTACK_HANDLER


=== STOP ===

{rounds_won == 5: gewonnen! | verloren!}

->END











 
 
 
 
=== function Counter1(ref x) ===

~x = 1
~return








=== function YBModeA() ===
{
- ybmode == false:
 ~ return 
 }
 
~Ycounter +=1
~Bcounter +=1
//resets Counter if they reach a threshold 
{Ycounter == 3 && Bcounter == 5: {Yreset()}}

{Ycounter:
- 4: ~Ycounter = 1
}
{Bcounter:
-6: ~Bcounter = 1
}

//sets AI-Attack according to the current Round and winchance 
{Ycounter:
-3: {attackY()}
}
{Bcounter:
-5: {attackB()}
}





=== function YBModeB() ===

~Ycounter   += 1
~Bcounter   += 1

~return
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
 
    
=== function attack0() ===

{Ycounter != 3 && Bcounter !=5 && roundCount == 2: {attack2()}}
{Ycounter != 3 && Bcounter !=5 && roundCount != 2: {attack1()}}


//attack functions of AI-Attack
=== function attack1() ===

{winchance:
-1:     ~attack = RANDOM(1,5)
-2:     ~attack = RANDOM(1,6)
-else:  ~attack = RANDOM(1,7)
}
~return


=== function attack2() ===

{winchance:
-1:     ~attack = RANDOM(6,10)
-2:     ~attack = RANDOM(5,10)
-else:  ~attack = RANDOM(4,10)
}
~return


=== function attackY() ===

~attack = RANDOM(11,16)
~return


=== function attackB() ===

~attack = RANDOM(15,20)
~return


//functions to see if AI-Attack is correct
=== function Ncheck() ===

{roundCount:
- 2:    {N7check()}
-else:  {N5check()}
}
~return


=== function N5check() ===

{attack:
- 1:    ~won = true
- 2:    ~won = true
- 3:    ~won = true
- 4:    ~won = true
- 5:    ~won = true
- else: ~won = false
}
~return


=== function N7check() ===

{attack:
- 6:    ~won = true
- 7:    ~won = true
- 8:    ~won = true
- 9:    ~won = true
- 10:   ~won = true
- else: ~won = false
}
~return


=== function Ycheck() ===

{attack:
- 11:   ~won = true
- 12:   ~won = true
- 13:   ~won = true
- 14:   ~won = true
- 15:   ~won = true
- else: ~won = false
}
~return


=== function Bcheck() ===

{attack:
- 16:   ~won = true
- 17:   ~won = true
- 18:   ~won = true
- 19:   ~won = true
- 20:   ~won = true
- else: ~won = false
}
~return


//resets Y and B counter if they re both at their max
=== function Yreset() ===

~Ycounter = 0
~Bcounter = 0
~return


=== ROUND_WON ===

Haha, das war falsch! #speaker:Flinta
Musst trinken! 

{rounds_won == 5: -> STOP}

~rounds_won += 1
~Ycounter = 0
~Bcounter = 0
~roundCount = 0
~Counter = 0


Jetzt bin ich dran!
-> CHOICES



