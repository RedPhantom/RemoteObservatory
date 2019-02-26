using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZWOptical.ASISDK;

namespace ZWOptical.ASISDK.ObjectModel
{
    public static class ASICameras
    {

        private static readonly Camera[] _cameras = new Camera[16]; //16

        public static int Count
        {
            get { return ASICameraDll2.ASIGetNumOfConnectedCameras(); }
        }

        public static Camera GetCameraByIndex(int cameraIndex)
        {  
            if (cameraIndex >= Count || cameraIndex < 0)
                throw new IndexOutOfRangeException();

            ASICameraDll2.ASI_CAMERA_INFO infoTemp;

            ASICameraDll2.ASIGetCameraProperty(out infoTemp, cameraIndex);

            int cameraId = infoTemp.CameraID;
            return _cameras[cameraId] ?? (_cameras[cameraId] = new Camera(cameraId));
        }
    
    }

    public enum ASI_STATUS
    {
        CLOSED = 0,
        OPENED,
        EXPOSURING
    }

    public class Camera
    {
        private readonly int _cameraId;
        private string _cachedName;
        private List<CameraControl> _controls;
        private ASICameraDll2.ASI_CAMERA_INFO? _info;
        private ASI_STATUS _status;
        public Camera(int cameraId)
        {
            _cameraId = cameraId;
        }

        private ASICameraDll2.ASI_CAMERA_INFO Info
        {
            // info is cached only while camera is open
            get {
                ASICameraDll2.ASI_CAMERA_INFO ci;
                ASICameraDll2.ASIGetCameraProperty(out ci, _cameraId);
                return ci;
            }
        }

        public ASI_STATUS status
        {
            get { return _status; }
        }

        public string Name
        {
            get { return Info.Name; } 
        }

        public bool IsColor
        {
            get { return Info.IsColorCam != ASICameraDll2.ASI_BOOL.ASI_FALSE; }
        }

        public bool HasST4
        {
            get { return Info.ST4Port != ASICameraDll2.ASI_BOOL.ASI_FALSE; }
        }

        public bool HasShutter
        {
            get { return Info.MechanicalShutter != ASICameraDll2.ASI_BOOL.ASI_FALSE; }
        }

        public bool HasCooler
        {
            get { return Info.IsCoolerCam != ASICameraDll2.ASI_BOOL.ASI_FALSE; }
        }

        public bool IsUSB3
        {
            get { return Info.IsUSB3Host != ASICameraDll2.ASI_BOOL.ASI_FALSE; }
        }

        public int CameraId
        {
            get { return _cameraId; }
        }

        public ASICameraDll2.ASI_BAYER_PATTERN BayerPattern
        {
            get { return Info.BayerPattern; }
        }

        //public class Size
        //{
        //    public int Width;
        //    public int Height;

        //    public Size(int Width, int Height)
        //    {
        //        this.Width = Width;
        //        this.Height = Height;
        //    }

        //    public Size() { }
        //}

        public Size Resolution
        {
            get
            {
                var info = Info;
                return new Size(info.MaxWidth, info.MaxHeight);
            }
        }

        public double PixelSize
        {
            get { return Info.PixelSize; }
        }

        public List<int> SupportedBinFactors
        {
            get { return Info.SupportedBins.TakeWhile(x => x != 0).ToList(); }
        }

        public List<ASICameraDll2.ASI_IMG_TYPE> SupportedImageTypes
        {
            get {
                
                return Info.SupportedVideoFormat.TakeWhile(x => x != ASICameraDll2.ASI_IMG_TYPE.ASI_IMG_END).ToList(); }
        }

        public ASICameraDll2.ASI_EXPOSURE_STATUS ExposureStatus
        {
            get {

                ASICameraDll2.ASI_EXPOSURE_STATUS status;
                ASICameraDll2.ASIGetExpStatus(_cameraId, out status);

                if (status != ASICameraDll2.ASI_EXPOSURE_STATUS.ASI_EXP_WORKING)
                    _status = ASI_STATUS.OPENED;
                return status;
            }
        }

        public void OpenCamera()
        {
            ASICameraDll2.ASIOpenCamera(_cameraId);

            ASICameraDll2.ASI_CAMERA_INFO ci;
            ASICameraDll2.ASIGetCameraProperty(out ci, _cameraId);
            _info = ci;

            ASICameraDll2.ASIInitCamera(_cameraId);
            _status = ASI_STATUS.OPENED;
        }

        public void CloseCamera()
        {
            _info = null;
            _controls = null;
            ASICameraDll2.ASICloseCamera(_cameraId);
            _status = ASI_STATUS.CLOSED;
        }

        public List<CameraControl> Controls
        {
            get
            {
                if (_controls == null || _cachedName != Name)
                {
                    _cachedName = Name;
                    int cc;
                    ASICameraDll2.ASIGetNumOfControls(_cameraId, out cc);

                    _controls = new List<CameraControl>();
                    for (int i = 0; i < cc; i++)
                    {
                        _controls.Add(new CameraControl(_cameraId, i));
                    }
                }

                return _controls;
            }
        }

        //public class Point
        //{
        //    public int X = 0;
        //    public int Y = 0;

        //    public Point (int X, int Y)
        //    {
        //        this.X = X;
        //        this.Y = Y;
        //    }

        //    public Point() { }
        //}

        public Point StartPos
        {
            get {
                Point p = new Point();
                ASICameraDll2.ASIGetStartPos(_cameraId, out p.X, out p.Y);
                return p;
            }
            set { ASICameraDll2.ASISetStartPos(_cameraId, value.X, value.Y); }
        }

        public CaptureAreaInfo CaptureAreaInfo
        {
            get
            {
                int bin;
                ASICameraDll2.ASI_IMG_TYPE imageType;
                Size s = new Size();

                ASICameraDll2.ASIGetROIFormat(_cameraId, out s.Width, out s.Height, out bin, out imageType);
                return new CaptureAreaInfo(s, bin, imageType);
            }
            set
            {
                ASICameraDll2.ASISetROIFormat(_cameraId, value.Size.Width, value.Size.Height, value.Binning, value.ImageType);
            }
        }

        //public int DroppedFrames
        //{
        //    get { return ASICameraDll2.GetDroppedFrames(_cameraId); }
        //}

        //public bool EnableDarkSubtract(string darkImageFilePath)
        //{
        //    return ASICameraDll2.EnableDarkSubtract(_cameraId, darkImageFilePath);
        //}

        //public void DisableDarkSubtract()
        //{
        //    ASICameraDll.DisableDarkSubtract(_cameraId);
        //}

        public void StartVideoCapture()
        {
            ASICameraDll2.ASIStartVideoCapture(_cameraId);
            _status = ASI_STATUS.EXPOSURING;
        }

        public void StopVideoCapture()
        {
            ASICameraDll2.ASIStopVideoCapture(_cameraId);
            _status = ASI_STATUS.OPENED;
        }

        public bool GetVideoData(IntPtr buffer, int bufferSize, int waitMs)
        {
            var v = ASICameraDll2.ASIGetVideoData(_cameraId, buffer, bufferSize, waitMs);
            if (v == ASICameraDll2.ASI_ERROR_CODE.ASI_SUCCESS)
                return true;
            else return false;
        }

        public void PulseGuideOn(ASICameraDll2.ASI_GUIDE_DIRECTION direction)
        {
            ASICameraDll2.ASIPulseGuideOn(_cameraId, direction);
        }

        public void PulseGuideOff(ASICameraDll2.ASI_GUIDE_DIRECTION direction)
        {
            ASICameraDll2.ASIPulseGuideOff(_cameraId, direction);
        }

        public void StartExposure(ASICameraDll2.ASI_BOOL isDark)
        {
            ASICameraDll2.ASIStartExposure(_cameraId, isDark);
            _status = ASI_STATUS.EXPOSURING;
        }

        public void StopExposure()
        {
            ASICameraDll2.ASIStopExposure(_cameraId);
        }

        public bool GetExposureData(IntPtr buffer, int bufferSize)
        {
            var v = ASICameraDll2.ASIGetDataAfterExp(_cameraId, buffer, bufferSize);

            if (v == ASICameraDll2.ASI_ERROR_CODE.ASI_SUCCESS)
                return true;
            else return false;
        }

        public CameraControl GetControl(ASICameraDll2.ASI_CONTROL_TYPE controlType)
        {
            return Controls.FirstOrDefault(x => x.ControlType == controlType);
        }
    }

    public class Point
    {
        public int X;
        public int Y;

        public Point (int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Point() { }
    }

    public class CaptureAreaInfo
    {
        public Size Size { get; set; }
        public int Binning { get; set; }
        public ASICameraDll2.ASI_IMG_TYPE ImageType { get; set; }

        public CaptureAreaInfo(Size size, int binning, ASICameraDll2.ASI_IMG_TYPE imageType)
        {
            Size = size;
            Binning = binning;
            ImageType = imageType;
        }
    }

    public class CameraControl
    {
        private readonly int _cameraId;
        private ASICameraDll2.ASI_CONTROL_CAPS _props;
        private bool _auto;

        public CameraControl(int cameraId, int controlIndex)
        {
            _cameraId = cameraId;

            ASICameraDll2.ASIGetControlCaps(_cameraId, controlIndex, out _props);
            //_auto = GetAutoSetting();
        }

        public string Name { get { return _props.Name; } }
        public string Description { get { return _props.Description; } }
        public int MinValue { get { return _props.MinValue; } }
        public int MaxValue { get { return _props.MaxValue; } }
        public int DefaultValue { get { return _props.DefaultValue; } }
        public ASICameraDll2.ASI_CONTROL_TYPE ControlType { get { return _props.ControlType; } }
        public bool IsAutoAvailable { get { return _props.IsAutoSupported != ASICameraDll2.ASI_BOOL.ASI_FALSE; } }
        public bool Writeable { get { return _props.IsWritable != ASICameraDll2.ASI_BOOL.ASI_FALSE; } }

        public int Value
        {
            get
            {
                bool isAuto;
                return ASICameraDll2.ASIGetControlValue(_cameraId, _props.ControlType);
            }
            set
            {
                ASICameraDll2.ASISetControlValue(_cameraId, _props.ControlType, value);                
            }
        }

        //public bool IsAuto
        //{
        //    get 
        //    {
        //        return _auto;
        //    }
        //    set
        //    {
        //        _auto = value;
        //        ASICameraDll.SetControlValue(_cameraId, _props.ControlType, Value, value);
        //    }
        //}

        //private bool GetAutoSetting()
        //{
        //    bool isAuto;
        //    ASICameraDll2.ASIGetControlValue(_cameraId, _props.ControlType, out isAuto);
        //    return isAuto;
        //}
    }
}
