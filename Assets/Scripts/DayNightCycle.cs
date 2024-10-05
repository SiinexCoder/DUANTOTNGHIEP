using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextMeshProUGUI;



public class DayNightCycle : MonoBehaviour
{
   public TextMeshProUGUI timeDisplay; //Display time
   public TextMeshProUGUI dayDisplay; //display day
   public Volume ppv;

   public float tick;
   public float seconds;
   public int mins;
   public int hours;
   public int days = 1;

   public bool activateLights; // check if light are on 
   public GameObject[] lights; //all the light we want on when its dark
   public SpriteRenderer[] stars; //star rendeer

   void Start()
    {
        ppv = gameObject.GetComponent<volume>();

    }
    void FixedUpdate()
    {
        CalcTime();
        DisplayTime();
    }

    public void CalcTime()
    {
        seconds +=Time.fixedDeltaTime * tick; 

        if(seconds >=60) // 60 sec =1 min
        {
            seconds =0;
            mins += 1;
        }
        if (mins >= 60) // 60 mins = 1hr
        {
            mins = 0;
            hours += 1;
        }
        if( hours >= 60) // 60 hours = 1 day
        {
            hours = 0;
            days += 1;
        }
        ControlPPV();
    }


    public void ControlPPV()
    {
        //ppv.wigth=0
        if(hours>=21 && hours<22) //dusk at 21:00/9pm    until 22:00/10pm
        {
            ppv.weight = (float)mins /60;
            for(int i = 0; i < stars.Lenght;i++)
            {
                stars[i].color =new Color(stars[i].color.r, stars[i].color.g,stars[i].color.b,(float )mins / 60);
            }
            if(activateLights == false) // if light havent been turned on
            {
                if(mins > 45)// wait untill prety dark
                {
                    for(int i = 0; i < lights.Lenght; i++)
                    {
                        lights[i].SetActive(true); // turn on all
                    }
                    activateLights = true;
                }
            }
        }


       if(hours>=6 && hours<7)// Dawn at 6:00 6am  - untill 7:0 //7pm
       {
        ppv.weight =1 - (float)mins/60 ; // we mins 1 because we want it to go from 1 -0 
        for(int i = 0; i < stars.Lenght; i++)
        {
            stars[i].color = new Color(stars[i].color.r, stars[i].color.g, stars[i].color.b,1-(float)mins/60);

        }
        if (activateLights == true)
        {
            if(mins > 45)
            {
                for(int i = 0; i < lights.Lenght; i++)
                {
                    lights[i].SetActive(false); //shut them off
                }
                activateLight = false;
            }
        }
       }
    }

    public void DisplayTime() //show time and day in ui
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}",hours , mins); 
        dayDisplay.text ="Day:"+days;
    }
}
