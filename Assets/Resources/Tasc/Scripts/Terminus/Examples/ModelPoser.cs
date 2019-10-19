using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class ModelPoser : Terminus
    {
        public bool isHided = false;
        //float headToArm;
        //float headToShoulder;

        GameObject modelHead;
        GameObject modelLeftHand;
        GameObject modelRightHand;
        GameObject modelLeftShoulder;
        GameObject modelRightShoulder;
        GameObject modelLeftElbow;
        GameObject modelRightElbow;
        GameObject modelWaist;

        int numberOfBodyPart = 8;
        bool initialized = false;

        Vector3[] initialPose;

        ////////////////////////////////////////////////////////////////////////////////////
        public void takePose(int index)
        {
            loadOrientations(initialPose);
            if (index == 0)
                makeFoldPose();
            else if (index == 1)
                makeTriangleLeft();
            else if (index == 2)
                makeLeftArmStraight();//loadOrientations(initialPose);//
            else if (index == 3)
                makeArmsUp();
            else if (index == 4)
                makeHeadUp();
        }

        ////////////////////////////////////////////////////////////////////////////////////

        void Start()
        {
            initialize();
        }

        public void initialize()
        {
            if (initialized)
                return;
            modelHead = GameObject.Find("EthanHead").gameObject;
            modelLeftHand = GameObject.Find("EthanLeftHand").gameObject;
            modelRightHand = GameObject.Find("EthanRightHand").gameObject;
            modelLeftShoulder = GameObject.Find("EthanLeftShoulder").gameObject;
            modelRightShoulder = GameObject.Find("EthanRightShoulder").gameObject;
            modelLeftElbow = GameObject.Find("EthanLeftForeArm").gameObject;
            modelRightElbow = GameObject.Find("EthanRightForeArm").gameObject;
            modelWaist = GameObject.Find("EthanSpine1").gameObject;
            /*
            modelHead = GameObject.Find("MHuman_Head").gameObject;
            modelLeftHand = GameObject.Find("MHuman_L_Hand").gameObject;
            modelRightHand = GameObject.Find("MHuman_R_Hand").gameObject;
            modelLeftShoulder = GameObject.Find("MHuman_L_Shoulder").gameObject;
            modelRightShoulder = GameObject.Find("MHuman_R_Shoulder").gameObject;
            modelLeftElbow = GameObject.Find("MHuman_L_Elbow").gameObject;
            modelRightElbow = GameObject.Find("MHuman_R_Elbow").gameObject;
            modelWaist = GameObject.Find("MHuman_Spine1").gameObject;
            */
            initialPose = new Vector3[numberOfBodyPart];
            saveOrientations(initialPose);
            if (isHided)
                hide();
            initialized = true;
        }

        void saveOrientations(Vector3[] quaternions)
        {
            if (quaternions.Length != numberOfBodyPart)
                return;
            quaternions[0] = modelHead.transform.eulerAngles;
            quaternions[1] = modelLeftHand.transform.eulerAngles;
            quaternions[2] = modelRightHand.transform.eulerAngles;
            quaternions[3] = modelLeftShoulder.transform.eulerAngles;
            quaternions[4] = modelRightShoulder.transform.eulerAngles;
            quaternions[5] = modelLeftElbow.transform.eulerAngles;
            quaternions[6] = modelRightElbow.transform.eulerAngles;
            quaternions[7] = modelWaist.transform.eulerAngles;
            //Debug.Log("Initial pose saved.");
        }

        void loadOrientations(Vector3[] quaternions)
        {
            if (quaternions.Length != numberOfBodyPart)
                return;
            modelHead.transform.rotation = Quaternion.Euler(quaternions[0]);
            modelLeftHand.transform.rotation = Quaternion.Euler(quaternions[1]);
            modelRightHand.transform.rotation = Quaternion.Euler(quaternions[2]);
            modelLeftShoulder.transform.rotation = Quaternion.Euler(quaternions[3]);
            modelRightShoulder.transform.rotation = Quaternion.Euler(quaternions[4]);
            modelLeftElbow.transform.rotation = Quaternion.Euler(quaternions[5]);
            modelRightElbow.transform.rotation = Quaternion.Euler(quaternions[6]);
            modelWaist.transform.rotation = Quaternion.Euler(quaternions[7]);
        }

        /*
        public void setHeadToArm(float value)
        {
            headToArm = value;
        }

        public void setHeadToShoulder(float value)
        {
            headToShoulder = value;
        }

        public float getHeadToArm()
        {
            return headToArm;
        }

        public float getHeadToShoulder()
        {
            return headToShoulder;
        }
        */

        public Vector3 getHeadPos()
        {
            return modelHead.transform.position;
        }

        public Vector3 getHeadLookAt()
        {
            return GameObject.Find("BreathBallPosition").transform.position;
        }

        public Vector3 getLeftHandPos()
        {
            return modelLeftHand.transform.position;
        }

        public Vector3 getRightHandPos()
        {
            return modelRightHand.transform.position;
        }

        public void show()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = true;
        }

        public void hide()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = false;
        }

        /*
        public void calibrate(CalibrationData data)
        {
            if (initialized)
                loadOrientations(initialPose);
            else
                initialize();
            transform.localScale = new Vector3(data.scale, data.scale, data.scale);
            modelLeftShoulder.transform.position = new Vector3(modelHead.transform.position.x - data.headToShoulder, modelLeftShoulder.transform.position.y, modelLeftShoulder.transform.position.z);
            modelRightShoulder.transform.position = new Vector3(modelHead.transform.position.x + data.headToShoulder, modelRightShoulder.transform.position.y, modelRightShoulder.transform.position.z);
            modelLeftHand.transform.position = new Vector3(modelHead.transform.position.x - data.headToArm, modelLeftHand.transform.position.y, modelLeftHand.transform.position.z);
            modelRightHand.transform.position = new Vector3(modelHead.transform.position.x + data.headToArm, modelRightHand.transform.position.y, modelRightHand.transform.position.z);
        }
        */

        public void makeArmsForward()
        {
            loadOrientations(initialPose);
            modelLeftShoulder.transform.Rotate(new Vector3(90, 0, 0));
            modelRightShoulder.transform.Rotate(new Vector3(-90, 0, 0));
        }

        public void makeArmsSide()
        {
            loadOrientations(initialPose);
        }

        public void makeArmsUp()
        {
            loadOrientations(initialPose);
            modelLeftShoulder.transform.Rotate(new Vector3(0, -100, -90));
            modelRightShoulder.transform.Rotate(new Vector3(0, 100, -90));
        }

        public void makeTriangleLeft()
        {
            loadOrientations(initialPose);
            modelLeftShoulder.transform.Rotate(new Vector3(0, -30, 0));
            modelWaist.transform.Rotate(new Vector3(0, -70, 0));
            modelHead.transform.Rotate(new Vector3(-90, 0, 0));
        }

        public void makeTriangleRight()
        {
            loadOrientations(initialPose);
            modelWaist.transform.Rotate(new Vector3(0, 90, 0));
            modelHead.transform.Rotate(new Vector3(0, 0, -90));
        }

        public void makeFoldPose()
        {
            makeArmsForward();
            modelWaist.transform.Rotate(new Vector3(0, 0, 70));
        }

        public void makeLeftArmStraight()
        {
            loadOrientations(initialPose);
            modelLeftShoulder.transform.Rotate(new Vector3(0, -90, -90));
            modelRightShoulder.transform.Rotate(new Vector3(0, 30, 0));
            modelRightElbow.transform.Rotate(new Vector3(0, -100, 0));
            modelWaist.transform.Rotate(new Vector3(0, 30, 0));
        }

        public void makeRightArmStraight()
        {
            loadOrientations(initialPose);
            modelRightShoulder.transform.Rotate(new Vector3(-90, -90, 0));
            modelLeftShoulder.transform.Rotate(new Vector3(0, -30, 0));
            modelLeftElbow.transform.Rotate(new Vector3(0, -100, 0));
            modelWaist.transform.Rotate(new Vector3(0, -30, 0));
        }

        public void makeHeadUp()
        {
            loadOrientations(initialPose);
            modelRightShoulder.transform.Rotate(new Vector3(0, -30, 0));
            modelRightElbow.transform.Rotate(new Vector3(0, -100, 0));
            modelLeftShoulder.transform.Rotate(new Vector3(0, 30, 0));
            modelLeftElbow.transform.Rotate(new Vector3(0, 100, 0));
            modelHead.transform.Rotate(new Vector3(0, 0, -60));
        }
    }

}
