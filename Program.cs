using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace talent_show
{

    // **************************************************
    //
    // Title: Finch Talent show
    // Description: Shows the prompt of finch robots actions
    // Application Type: Console
    // Author: Drzewiecki, Marcus
    // Dated Created: 2/18/2020
    // Last Modified: 2/26/2020
    //
    // **************************************************

    class Program
    {

        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;
            

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tg) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":

                        break;

                    case "d":

                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "g":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb)  move around");
                Console.WriteLine("\tc) re-connect robot");
                Console.WriteLine("\td) ");
                Console.WriteLine("\tg) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(myFinch);
                        break;

                    case "b":
                        DisplayFinchMovement(myFinch);
                        break;

                    case "c":
                        DisplayConnectFinchRobot(myFinch);
                        break;

                    case "d":

                        break;

                    case "g":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }
        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > movement                  *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
    
        static void DisplayFinchMovement(Finch finchRobot)
        {
            string userResponse;
            int leftSpeed;
            int rightSpeed;
            Console.CursorVisible = false;

            DisplayScreenHeader("movement");

            Console.WriteLine("\t how fast do you want the left wheel to go? (please answer from a speed rating of 1-255)");
            Console.WriteLine();
            userResponse = Console.ReadLine();
            leftSpeed = int.Parse(userResponse);

            Console.WriteLine("\t Now lets set the speed for the right wheel?(please answer from a speed rating of 1-255)");
            Console.WriteLine();
            userResponse = Console.ReadLine();
            rightSpeed = int.Parse(userResponse);

            DisplayContinuePrompt();

            int leftMotor = leftSpeed;
            int rightMotor = rightSpeed;
            finchRobot.setMotors(leftMotor, rightMotor);

            Console.WriteLine(" Press any key to disconnet Robot");
            Console.ReadKey();
            finchRobot.disConnect();

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will now light up your world and sing!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 60; lightSoundLevel < 500; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 200);
            }

            DisplayMenuPrompt("Talent Show Menu");
        }
          
            #endregion

            #region FINCH ROBOT MANAGEMENT

            /// <summary>
            /// *****************************************************************
            /// *               Disconnect the Finch Robot                      *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot">finch robot object</param>
            static void DisplayDisconnectFinchRobot(Finch finchRobot)
            {
                Console.CursorVisible = false;

                DisplayScreenHeader("Disconnect Finch Robot");

                Console.WriteLine("\tAbout to disconnect from the Finch robot.");
                DisplayContinuePrompt();

                finchRobot.disConnect();

                Console.WriteLine("\tThe Finch robot is now disconnect.");

                DisplayMenuPrompt("Main Menu");
            }

            /// <summary>
            /// *****************************************************************
            /// *                  Connect the Finch Robot                      *
            /// *****************************************************************
            /// </summary>
            /// <param name="finchRobot">finch robot object</param>
            /// <returns>notify if the robot is connected</returns>
            static bool DisplayConnectFinchRobot(Finch finchRobot)
            {
                Console.CursorVisible = false;

                bool robotConnected;

                DisplayScreenHeader("Connect Finch Robot");

                Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
                DisplayContinuePrompt();

                robotConnected = finchRobot.connect();

                // TODO test connection and provide user feedback - text, lights, sounds

                DisplayMenuPrompt("Main Menu");

                //
                // reset finch robot
                //
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();

                return robotConnected;
            }

            #endregion

            #region USER INTERFACE

            /// <summary>
            /// *****************************************************************
            /// *                     Welcome Screen                            *
            /// *****************************************************************
            /// </summary>
            static void DisplayWelcomeScreen()
            {
                Console.CursorVisible = false;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\tFinch Control");
                Console.WriteLine();

                DisplayContinuePrompt();
            }

            /// <summary>
            /// *****************************************************************
            /// *                     Closing Screen                            *
            /// *****************************************************************
            /// </summary>
            static void DisplayClosingScreen()
            {
                Console.CursorVisible = false;

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\tThank you for using Finch Control!");
                Console.WriteLine();

                DisplayContinuePrompt();
            }

            /// <summary>
            /// display continue prompt
            /// </summary>
            static void DisplayContinuePrompt()
            {
                Console.WriteLine();
                Console.WriteLine("\tPress any key to continue.");
                Console.ReadKey();
            }

            /// <summary>
            /// display menu prompt
            /// </summary>
            static void DisplayMenuPrompt(string menuName)
            {
                Console.WriteLine();
                Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
                Console.ReadKey();
            }

            /// <summary>
            /// display screen header
            /// </summary>
            static void DisplayScreenHeader(string headerText)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\t\t" + headerText);
                Console.WriteLine();
            }

            #endregion
        }
    }
