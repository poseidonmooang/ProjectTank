using GoogleCloudStreamingSpeechToText;
using System.Collections;
using UnityEngine;

using UnityEngine.UI;


namespace ChobiAssets.PTM
{

	public class Drive_Control_Input_03_Single_Stick_CS : Drive_Control_Input_02_Keyboard_Pressing_CS
    {
        public StreamingRecognizer streamingRecognizer;
        public Text resultText;


        void Start()
        {
            if (streamingRecognizer == null)
            {
                streamingRecognizer = GameObject.Find("STT").GetComponent<StreamingRecognizer>();
            }
        }

        bool driveBool = false;

        void Update(){
            if (Input.GetButtonDown("Sidong"))
            {
                driveBool = !driveBool;
            }
            if (Input.GetButtonDown("Stt Start"))
            {
                streamingRecognizer.StartListening();
            }
            if (Input.GetButtonDown("Stt Stop"))
            {
                streamingRecognizer.StopListening();
            }

            if (resultText != null && streamingRecognizer != null && streamingRecognizer.sttStart)
            {
                resultText.text = "Result: " + streamingRecognizer.resultStt;
            }

        }
		
		public override void Drive_Input()
		{
            if (driveBool)
            {
                if (!streamingRecognizer.sttStart)
                {
                    //Set "vertical".
                    vertical = Input.GetAxis("Vertical");
                    vertical = Mathf.Clamp(vertical, -0.5f, 1.0f);

                    // Set "horizontal".
                    horizontal = Input.GetAxis("Horizontal");
                }
                else
                {
                    switch (streamingRecognizer.resultStt)
                    {
                        case "악보":
                        case "아프로":
                        case "앞으로":
                            vertical = Mathf.Clamp(vertical + 0.01f, -0.5f, 0.5f);
                            Debug.Log("앞으로");
                            break;
                        case "엄청":
                        case "멈춰":
                            vertical = 0.0f;
                            horizontal = 0.0f;
                            Debug.Log("멈춰");
                            break;
                        case "위로":
                        case "뒤로":
                            vertical = Mathf.Clamp(vertical - 0.01f, -0.5f, 0.5f);
                            Debug.Log("뒤로");
                            break;
                        case "아해줘":
                        case "좌회전":
                            horizontal = Mathf.Clamp(horizontal - 0.05f, -1.0f, 1.0f);
                            Debug.Log("좌회전");
                            Invoke("Stt_change", 1f);
                            break;
                        case "우해줘":
                        case "구혜선":
                        case "우회전":
                            horizontal = Mathf.Clamp(horizontal + 0.05f, -1.0f, 1.0f);
                            Debug.Log("우회전");
                            Invoke("Stt_change", 1f);
                            break;

                        default:
                            break;
                    }
                }




            Set_Values();
            }
		}

        void Stt_change()
        {
            streamingRecognizer.resultStt = "";
            horizontal = 0.0f;
        }


        protected override void Brake_Turn()
        {
            if (horizontal < 0.0f)
            { // Left turn.
                controlScript.L_Input_Rate = 0.0f;
                controlScript.R_Input_Rate = vertical;
            }
            else
            { // Right turn.
                controlScript.L_Input_Rate = -vertical;
                controlScript.R_Input_Rate = 0.0f;
            }

            // Set the "Turn_Brake_Rate".
            controlScript.Turn_Brake_Rate = horizontal;
		}

	}

}
