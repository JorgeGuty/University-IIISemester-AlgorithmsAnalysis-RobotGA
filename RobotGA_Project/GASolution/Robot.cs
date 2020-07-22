namespace RobotGA_Project.GASolution
{
    public class Robot
    {
        
        public Battery Battery { get; set; }
        public Camera Camera { get; set; }
        public Engine Engine { get; set; }

        public Robot(int pCameraGenotype, int pBatteryGenotype, int pEngineGenotype)
        {
            int minValue = Constants.GenotypeMinvalue;
            int maxValue = Constants.GenotypeMaxValue;
            
            SetBattery(pBatteryGenotype,minValue,maxValue);
            SetCamera(pCameraGenotype,minValue,maxValue);
            SetEngine(pEngineGenotype,minValue,maxValue);
        }

        private void SetEngine(int pEngineGenotype, int pMinValue, int pMaxValue)
        {
            
            int interval = pMaxValue / Constants.EngineTypeQuantity;

            if (pMinValue <= pEngineGenotype && pEngineGenotype < interval)  // MinValue-interval
            {
                Engine = Constants.SmallEngine;
            }
            else if (interval <= pEngineGenotype && pEngineGenotype < interval * 2)  // interval-2(interval)
            {
                Engine = Constants.MediumEngine;
            }
            else if (interval * 2 <= pEngineGenotype && pEngineGenotype <= pMaxValue) // 2(interval)-MaxValue
            {
                Engine = Constants.BigEngine;
            }
        }
        
        private void SetCamera(int pCameraGenotype, int pMinValue, int pMaxValue)
        {

            int interval = pMaxValue / Constants.CameraTypeQuantity;

            if (pMinValue <= pCameraGenotype && pCameraGenotype < interval)
            {
                Camera = Constants.SmallCamera;
            }
            else if (interval <= pCameraGenotype && pCameraGenotype < interval * 2)
            {
                Camera = Constants.MediumCamera;
            }
            else if (interval * 2 <= pCameraGenotype && pCameraGenotype <= pMaxValue)
            {
                Camera = Constants.BigCamera;
            }
        }

        private void SetBattery(int pBatteryGenotype, int pMinValue, int pMaxValue)
        {
            int interval = pMaxValue / Constants.BatteryTypeQuantity;

            if (pMinValue <= pBatteryGenotype && pBatteryGenotype < interval)
            {
                Battery = Constants.CommonBattery;
            }
            else if (interval <= pBatteryGenotype && pBatteryGenotype < interval * 2)
            {
                Battery = Constants.MediumBattery;
            }
            else if (interval * 2 <= pBatteryGenotype && pBatteryGenotype <= pMaxValue)
            {
                Battery = Constants.SuperBattery;
            }
        }
        
    }
}