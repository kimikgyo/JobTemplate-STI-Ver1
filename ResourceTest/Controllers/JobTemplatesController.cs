using Common.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResourceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTemplatesController : ControllerBase
    {
        private List<JobTemplate> jobTemplatesAmkor = new List<JobTemplate>();
        private List<MissionTemplate> missionTemplatesAmkor = new List<MissionTemplate>();
        private List<JobTemplate> jobTemplatesSTI = new List<JobTemplate>();
        private List<MissionTemplate> missionTemplatesSTI = new List<MissionTemplate>();

        private List<Report> reports = null;

        // GET: api/<JobTemplatesController>
        [HttpGet("Amkor")]
        public ActionResult<List<JobTemplate>> GetAmkor()
        {
            //var jobTemplate = JobTemplateAddAmkor();
            List<JobTemplate> JobTemplateAddAmkor()
            {
                for (int i = 1; i < 14; i++)
                {
                    var jobTemplate = new JobTemplate
                    {
                        id = i,
                        group = "",
                        isLocked = false,
                    };

                    if (i == 1 || i == 2)
                    {
                        jobTemplate.type = JobTemplateType.Move.ToString();
                        if (i == 1)
                        {
                            //SimpleMove    같은 층 간 이동
                            jobTemplate.subType = JobTemplateSubType.SimpleMove.ToString();
                            //목적지 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove, false));
                        }
                        else
                        {
                            //MoveWithElevator  E/V 포함 다른 층 이동

                            jobTemplate.subType = JobTemplateSubType.MoveWithEV.ToString();

                            //E/V대기 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorWaitMove, false));
                            //E/V Call
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Call, true));
                            //E/V DoorOpen
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorOpen, true));
                            //E/V 탑승
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorEnterMove, true));
                            //E/V 탑승완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.EnterComplete, true));
                            //E/V 하차
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorExitMove, true));
                            //E/V 하차완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.ExitComplete, true));
                            //E/V DoorClose
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorClose, true));
                            //최종목적지 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove, false));
                        }
                    }
                    else if (i == 3 || i == 4 || i == 5 || i == 6)
                    {
                        jobTemplate.type = JobTemplateType.Transport.ToString();
                        if (i == 3)
                        {
                            //PickOnly   자재 픽업만 수행

                            jobTemplate.subType = JobTemplateSubType.PickOnly.ToString();

                            //Pick 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove, false));
                            //Pick 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Pick, true));
                        }
                        else if (i == 4)
                        {
                            //DropOnly	자재 Drop만 수행
                            jobTemplate.subType = JobTemplateSubType.DropOnly.ToString();
                            //Drop 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove, false));
                            //Drop 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Drop, true));
                        }
                        else if (i == 5)
                        {
                            //PickDrop	자재 Pick → Drop
                            jobTemplate.subType = JobTemplateSubType.PickDrop.ToString();
                            //Pick 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove, false));
                            //Pick 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Pick, true));
                            //Drop 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove, false));
                            //Drop 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Drop, true));
                        }
                        else if (i == 6)
                        {
                            // PickDropWithEV	자재 Pick → E/V → Drop
                            jobTemplate.subType = JobTemplateSubType.PickDropWithEV.ToString();

                            //Pick 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove, false));
                            //Pick 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Pick, true));
                            //E/V대기 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorWaitMove, false));
                            //E/V Call
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Call, true));
                            //E/V DoorOpen
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorOpen, true));
                            //E/V 탑승
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorEnterMove, true));
                            //E/V 탑승완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.EnterComplete, true));
                            //E/V 하차
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorExitMove, true));
                            //E/V 하차완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.ExitComplete, true));
                            //E/V DoorClose
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorClose, true));
                            //Drop 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove, false));
                            //Drop 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Drop, true));
                        }
                    }
                    else if (i == 7 || i == 8)
                    {
                        jobTemplate.type = JobTemplateType.Charge.ToString();
                        if (i == 7)
                        {
                            //GoToCharger	같은 층 충전소 이동
                            jobTemplate.subType = JobTemplateSubType.Charge.ToString();
                            //Charge 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ChargerMove, false));
                            //Charge 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Charge, false));
                        }
                        else
                        {
                            //GoToChargerWithEV	E/V 포함 충전소 이동

                            jobTemplate.subType = JobTemplateSubType.Charge.ToString();

                            //E/V대기 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorWaitMove, false));
                            //E/V Call
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Call, true));
                            //E/V DoorOpen
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorOpen, true));
                            //E/V 탑승
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorEnterMove, true));
                            //E/V 탑승완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.EnterComplete, true));
                            //E/V 하차
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorExitMove, true));
                            //E/V 하차완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.ExitComplete, true));
                            //E/V DoorClose
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorClose, true));
                            //Charge 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ChargerMove, false));
                            //Charge 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Charge, false));
                        }
                    }
                    else if (i == 9 || i == 10)
                    {
                        jobTemplate.type = JobTemplateType.Wait.ToString();
                        if (i == 9)
                        {
                            //WaitOnly	같은 층 대기
                            jobTemplate.subType = JobTemplateSubType.Wait.ToString();
                            //Wait 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.WaitMove, false));
                        }
                        else
                        {
                            //WaitWithEV E/ V 포함 대기 위치 이동

                            jobTemplate.subType = JobTemplateSubType.WaitWithEV.ToString();
                            //E/V대기 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorWaitMove, false));
                            //E/V Call
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Call, true));
                            //E/V DoorOpen
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorOpen, true));
                            //E/V 탑승
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorEnterMove, true));
                            //E/V 탑승완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.EnterComplete, true));
                            //E/V 하차
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorExitMove, true));
                            //E/V 하차완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.ExitComplete, true));
                            //E/V DoorClose
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorClose, true));
                            //Wait 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.WaitMove, false));
                        }
                    }
                    else if (i == 11 || i == 12)
                    {
                        jobTemplate.type = JobTemplateType.Reset.ToString();
                        if (i == 11)
                        {
                            //Reset	복구 위치 이동 (같은 층)
                            jobTemplate.subType = JobTemplateSubType.Reset.ToString();
                            //Reset 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ResetMove, false));
                            //Reset 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Reset, false));
                        }
                        else
                        {
                            //ResetWithEV	복구 위치 이동 (다른 층 + E/V)
                            jobTemplate.subType = JobTemplateSubType.ResetWithEV.ToString();
                            //E/V대기 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorWaitMove, false));
                            //E/V Call
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Call, true));
                            //E/V DoorOpen
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorOpen, true));
                            //E/V 탑승
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorEnterMove, true));
                            //E/V 탑승완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.EnterComplete, true));
                            //E/V 하차
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ElevatorExitMove, true));
                            //E/V 하차완료
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.ExitComplete, true));
                            //E/V DoorClose
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.DoorClose, true));
                            //Reset 위치 이동
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Move, MissionTemplateSubType.ResetMove, false));
                            //Reset 실행
                            jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Reset, false));
                        }
                    }
                    else if (i == 13)
                    {
                        //Cancel	현재 작업 취소
                        jobTemplate.type = JobTemplateType.Cancel.ToString();
                        jobTemplate.subType = JobTemplateSubType.Cancel.ToString();
                        //Cancel 실행
                        jobTemplate.missionTemplates.Add(MissionTempleateCreateAmkor(i, MissionTemplateType.Action, MissionTemplateSubType.Cancel, false));
                    }
                    jobTemplatesAmkor.Add(jobTemplate);
                }
                return jobTemplatesAmkor;
            }
            MissionTemplate MissionTempleateCreateAmkor(int JobTemplateId, MissionTemplateType type, MissionTemplateSubType SubType, bool look)
            {
                MissionTemplate missionTemplate = new MissionTemplate();

                //missionTemplate.jobTemplateId = JobTemplateId;
                missionTemplate.type = type.ToString();
                missionTemplate.subType = SubType.ToString();
                missionTemplate.isLook = look;

                switch (type)
                {
                    case MissionTemplateType.None:
                        break;

                    case MissionTemplateType.Move:
                        Parameta param = new Parameta();
                        param.key = "target";
                        missionTemplate.service = Service.Worker.ToString();
                        missionTemplate.parameters.Add(param);

                        break;

                    case MissionTemplateType.Action:
                        switch (SubType)
                        {
                            case MissionTemplateSubType.None:
                            case MissionTemplateSubType.Wait:
                            case MissionTemplateSubType.Pick:
                            case MissionTemplateSubType.Drop:
                            case MissionTemplateSubType.Charge:
                            case MissionTemplateSubType.Reset:
                            case MissionTemplateSubType.Cancel:
                                missionTemplate.service = Service.Worker.ToString();
                                break;

                            case MissionTemplateSubType.Call:
                            case MissionTemplateSubType.DoorOpen:
                            case MissionTemplateSubType.EnterComplete:
                            case MissionTemplateSubType.DoorClose:
                            case MissionTemplateSubType.ExitComplete:
                                missionTemplate.service = Service.Elevator.ToString();
                                break;
                        }
                        break;
                }

                missionTemplatesAmkor.Add(missionTemplate);

                return missionTemplate;
            }
            return JobTemplateAddAmkor();
        }

        // GET api/<JobTemplatesController>/5
        [HttpGet("STI")]
        public ActionResult<List<JobTemplate>> GetSTI()
        {
            reports = GetReports();

            List<JobTemplate> JobTemplateAddSTI()
            {
                for (int i = 1; i < 12; i++)
                {
                    var jobTemplate = new JobTemplate
                    {
                        id = i,
                        group = "",
                    };

                    if (i == 1)
                    {
                        jobTemplate.type = JobTemplateType.Move.ToString();
                        //SimpleMove    같은 층 간 이동
                        jobTemplate.subType = JobTemplateSubType.SimpleMove.ToString();
                        //목적지 이동
                        jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                      , null
                                                                                      , false
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
                        jobTemplatesSTI.Add(jobTemplate);
                    }
                    else if (i >= 2 && i <= 7)
                    {
                        //PickDrop	자재 Pick → Drop
                        jobTemplate.subType = JobTemplateSubType.PickDrop.ToString();

                        // 드럼 공급
                        if (i == 2)
                        {
                            jobTemplate.type = JobTemplateType.TransportChemicalSupply.ToString();
                            jobTemplate = TransportChemicalSupply(jobTemplate, i);
                            jobTemplatesSTI.Add(jobTemplate);
                        }
                        //드럼 회수
                        else if (i == 3)
                        {
                            jobTemplate.type = JobTemplateType.TransportChemicalRecovery.ToString();

                            jobTemplate = TransportChemicalRecovery(jobTemplate, i);
                            jobTemplatesSTI.Add(jobTemplate);
                        }
                        //슬러리 공급
                        else if (i == 4)
                        {
                            jobTemplate.type = JobTemplateType.TransportSlurrySupply.ToString();

                            jobTemplate = TransportSlurrySupply(jobTemplate, i);
                            jobTemplatesSTI.Add(jobTemplate);
                        }
                        //슬러리 회수
                        else if (i == 5)
                        {
                            jobTemplate.type = JobTemplateType.TransportSlurryRecovery.ToString();
                            jobTemplate = TransportSlurryRecovery(jobTemplate, i);
                            jobTemplatesSTI.Add(jobTemplate);
                        }
                        //아이세로 공급
                        else if (i == 6)
                        {
                            jobTemplate.type = JobTemplateType.TransportAicellomilimSupply.ToString();

                            jobTemplate = TransportAicellomilimSupply(jobTemplate, i);
                            jobTemplatesSTI.Add(jobTemplate);
                        }
                        //아이세로 회수
                        else if (i == 7)
                        {
                            jobTemplate.type = JobTemplateType.TransportAicellomilimRecovery.ToString();
                            jobTemplate = TransportAicellomilimRecovery(jobTemplate, i);
                            jobTemplatesSTI.Add(jobTemplate);
                        }
                    }
                    else if (i == 8)
                    {
                        jobTemplate.type = JobTemplateType.Transport.ToString();
                        jobTemplate.subType = JobTemplateSubType.DropOnly.ToString();
                        jobTemplate = Transport(jobTemplate, i);
                        jobTemplatesSTI.Add(jobTemplate);
                    }
                    else if (i == 9)
                    {
                        jobTemplate.type = JobTemplateType.Charge.ToString();
                        //GoToCharger	같은 층 충전소 이동
                        jobTemplate.subType = JobTemplateSubType.Charge.ToString();
                        //Charge 위치 이동
                        jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , false
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
                        ////Charge 실행
                        //jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.Charge
                        //                                                            , "CHARGEREQUEST", "action", "CHARGEREQUEST", "targetlevel", null));
                        jobTemplatesSTI.Add(jobTemplate);
                    }
                    else if (i == 10)
                    {
                        jobTemplate.type = JobTemplateType.Wait.ToString();
                        //WaitOnly	같은 층 대기
                        jobTemplate.subType = JobTemplateSubType.Wait.ToString();
                        //Wait 위치 이동
                        jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , false
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
                        jobTemplatesSTI.Add(jobTemplate);
                    }
                    else if (i == 11)
                    {
                        jobTemplate.type = JobTemplateType.Reset.ToString();
                        ////Reset	복구 위치 이동 (같은 층)
                        jobTemplate.subType = JobTemplateSubType.Reset.ToString();
                        ////Reset 위치 이동
                        jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.ResetMove
                                                                                    , null
                                                                                    , false
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
                        jobTemplatesSTI.Add(jobTemplate);
                        //Reset 실행
                        //jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.Reset
                        //                                                            , null, null, null, null));
                    }
                }
                return jobTemplatesSTI;
            }

            return JobTemplateAddSTI();
        }

        private JobTemplate TransportChemicalSupply(JobTemplate jobTemplate, int i)
        {
            //버퍼 대기 포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27b")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////PIO 시작,롤러UP 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "공급 액션(버퍼 PIO시작)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , createPostReport(reports, null)));

            //출발지이동
            if (i == 2) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireStarted"])));

            ////Get 실행 (출발지->AGV)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "GetDrum"
                                                                                    , "공급 액션(버퍼 GET)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //PIO완료 위치 이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27c")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO완료,롤러다운 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "공급 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //2DrumCapSearch
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "CapSearch"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a26f")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //드럼바코더리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleDepositStarted"])
                                                                                    , createPostReport(reports, null)));
            //,Cap정렬실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 CAP 정렬)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_ALIGN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a25d")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,DoorOpen"
                                                                                    , "공급 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a265")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //설비바코드리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading"
                                                                                    , "공급 액션(설비 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_EQP_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //목적지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //드럼이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "공급 액션(설비 DRUM 이동)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a265")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDepositCompleted"])
                                                                                    , createPostReport(reports, null)));
            //UR작업[CapOpen,커플러체결]
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 CAP 열기)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_OPEN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 COUPLER 체결)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_COUPLER_ATTACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a273")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO완료,Close
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted"
                                                                                    , "공급 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                   , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            return jobTemplate;
        }

        private JobTemplate TransportChemicalRecovery(JobTemplate jobTemplate, int i)
        {
            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a25d")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,Dooropen"
                                                                                    , "회수 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , createPostReport(reports, null)));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a265")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //커플러해제,CapClose
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "Capclose"
                                                                                    , "회수 액션(설비 COUPLER 분리)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_COUPLER_DETACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                 //, "Capclose"
                                                                                 , "회수 액션(설비 CAP 닫기)"
                                                                                 , true
                                                                                 , createParameta("type", "CDRUM")
                                                                                 , createParameta("linkedFacility", null)
                                                                                 , createParameta("action", "GET_ACTION_CAP_CLOSE")
                                                                                 , createParameta("drumKeyCode", null)
                                                                                 , createPreReport(reports, null)
                                                                                 , createPostReport(reports, null)));

            //출발지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            ////Get 컨베이어 -> AGV
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "GetDrum"
                                                                                    , "회수 액션(설비DRUM이동)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleAcquireStarted"])
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a273")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //DoorClose,Pio완료
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "DoorClose,PIOCompleted"
                                                                                    , "회수 액션(설비 PIO 완료) "
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27b")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //PIO 시작,롤러UP
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "회수 액션(버퍼 PIO시작)"
                                                                                     , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleDepositStarted"])));
            //목적지
            if (i == 3) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PUT AGV -> Buffer
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "회수 액션(버퍼 PUT)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                   , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //PIO완료포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27c")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            ////PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "회수 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleDepositCompleted"])));
            return jobTemplate;
        }

        private JobTemplate TransportSlurrySupply(JobTemplate jobTemplate, int i)
        {
            //버퍼 대기 포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27b")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////PIO 시작,롤러UP 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "공급 액션(버퍼 PIO시작)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , createPostReport(reports, null)));

            //출발지
            if (i == 4) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //Get
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "GETDrum"
                                                                                    , "공급 액션(버퍼 GET)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleAcquireStarted"])
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //PIO완료포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27c")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            ///PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "공급 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //SlurryCapSearch
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "CapSearch"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a26f")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////슬러리바코더리딩,Cap정렬
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleDepositStarted"])
                                                                                    , createPostReport(reports, null)));


            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                //, "Barcodereading,Capalignment"
                                                                                , "공급 액션(드럼 CAP 정렬)"
                                                                                , true
                                                                                , createParameta("type", "SDRUM")
                                                                                , createParameta("linkedFacility", null)
                                                                                , createParameta("action", "PUT_ACTION_CAP_ALIGN")
                                                                                , createParameta("drumKeyCode", null)
                                                                                , createPreReport(reports, null)
                                                                                , createPostReport(reports, null)));

            //Slurry_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a270")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,Dooropen"
                                                                                    , "공급 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //Slurry_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a259")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //설비바코드리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading"
                                                                                    , "공급 액션(설비 BARCODE 읽기)"
                                                                                     , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_EQP_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //목적지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //드럼이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "공급 액션(설비 DRUM 이동)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //Slurry_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a259")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDepositCompleted"])
                                                                                    , createPostReport(reports, null)));

            //UR작업[CapOpen,커플러체결]
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "CapOpen"
                                                                                    , "공급 액션(설비 CAP 열기)"
                                                                                     , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_OPEN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                  //, "CapOpen"
                                                                                  , "공급 액션(설비 COUPLER 체결)"
                                                                                   , true
                                                                                  , createParameta("type", "SDRUM")
                                                                                  , createParameta("linkedFacility", null)
                                                                                  , createParameta("action", "PUT_ACTION_COUPLER_ATTACH")
                                                                                  , createParameta("drumKeyCode", null)
                                                                                  , createPreReport(reports, null)
                                                                                  , createPostReport(reports, null)));


            //Slurry_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_4(End)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a262")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO완료,Close
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted"
                                                                                    , "공급 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            return jobTemplate;
        }

        private JobTemplate TransportSlurryRecovery(JobTemplate jobTemplate, int i)
        {
            //Slurry_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "Slurry_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a270")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,DoorOpen"
                                                                                    , "회수 액션(설비 PIO 시작)"
                                                                                   , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , createPostReport(reports, null)));

            //Slurry_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "Slurry_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a259")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            ////커플러해제,CapClose
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "CapClose"
                                                                                    , "회수 액션(설비 COUPLER 분리)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_COUPLER_DETACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                              //, "CapClose"
                                                                              , "회수 액션(설비 CAP 닫기)"
                                                                              , true
                                                                              , createParameta("type", "SDRUM")
                                                                              , createParameta("linkedFacility", null)
                                                                              , createParameta("action", "GET_ACTION_CAP_CLOSE")
                                                                              , createParameta("drumKeyCode", null)
                                                                              , createPreReport(reports, null)
                                                                              , createPostReport(reports, null)));

            ////출발지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                            , null
                                                                            , true
                                                                            , createParameta("target", null)
                                                                            , createParameta(null, null)
                                                                            , createParameta(null, null)
                                                                            , createParameta(null, null)
                                                                            , createPreReport(reports, null)
                                                                            , createPostReport(reports, null)));

       

            ////Get
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                         //, "GetDrum"
                                                                         , "회수 액션(설비DRUM이동)"
                                                                         , true
                                                                         , createParameta("type", "SDRUM")
                                                                         , createParameta("linkedFacility", null)
                                                                         , createParameta("action", "GET_ACTION_DRUM_MOVE")
                                                                         , createParameta("drumKeyCode", null)
                                                                         , createPreReport(reports, ["VehicleAcquireStarted"])
                                                                         , createPostReport(reports, ["CarrierInstalled"])));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                        , "Slurry_4(End)"
                                                                        , true
                                                                        , createParameta("target", "69006307ac8ad18ebac0a262")
                                                                        , createParameta(null, null)
                                                                        , createParameta(null, null)
                                                                        , createParameta(null, null)
                                                                        , createPreReport(reports, null)
                                                                        , createPostReport(reports, null)));

            //DoorClose,Pio완료
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "Doorclose,PIOCompleted"
                                                                                    , "회수 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                     , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27b")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////PIO 시작,롤러UP
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "회수 액션(버퍼 PIO시작)"
                                                                                     , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleDepositStarted"])
                                                                                     , createPostReport(reports, null)));

            //목적지
            if (i == 5) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            ////PUT
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "회수 액션(버퍼 PUT)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //PIO완료포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27c")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "회수 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                     , createPostReport(reports, ["VehicleDepositCompleted"])));

            return jobTemplate;
        }


        private JobTemplate TransportAicellomilimSupply(JobTemplate jobTemplate, int i)
        {
            //버퍼 대기 포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27b")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////PIO 시작,롤러UP 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "공급 액션(버퍼 PIO시작)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , createPostReport(reports, null)));

            //출발지이동
            if (i == 6) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireStarted"])));

            ////Get 실행 (출발지->AGV)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "GetDrum"
                                                                                    , "공급 액션(버퍼 GET)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //PIO완료 위치 이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27c")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO완료,롤러다운 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "공급 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //2DrumCapSearch
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "CapSearch"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a26f")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //드럼바코더리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleDepositStarted"])
                                                                                    , createPostReport(reports, null)));
            //,Cap정렬실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 CAP 정렬)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_ALIGN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a25d")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,DoorOpen"
                                                                                    , "공급 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a265")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //설비바코드리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading"
                                                                                    , "공급 액션(설비 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_EQP_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //목적지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //드럼이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "공급 액션(설비 DRUM 이동)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a265")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDepositCompleted"])
                                                                                    , createPostReport(reports, null)));
            //UR작업[CapOpen,커플러체결]
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 CAP 열기)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_OPEN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 COUPLER 체결)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_COUPLER_ATTACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a273")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PIO완료,Close
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted"
                                                                                    , "공급 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                   , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            return jobTemplate;
        }

        private JobTemplate TransportAicellomilimRecovery(JobTemplate jobTemplate, int i)
        {
            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a25d")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,Dooropen"
                                                                                    , "회수 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , createPostReport(reports, null)));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a265")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //커플러해제,CapClose
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "Capclose"
                                                                                    , "회수 액션(설비 COUPLER 분리)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_COUPLER_DETACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                 //, "Capclose"
                                                                                 , "회수 액션(설비 CAP 닫기)"
                                                                                 , true
                                                                                 , createParameta("type", "IDRUM")
                                                                                 , createParameta("linkedFacility", null)
                                                                                 , createParameta("action", "GET_ACTION_CAP_CLOSE")
                                                                                 , createParameta("drumKeyCode", null)
                                                                                 , createPreReport(reports, null)
                                                                                 , createPostReport(reports, null)));

            //출발지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            ////Get 컨베이어 -> AGV
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "GetDrum"
                                                                                    , "회수 액션(설비DRUM이동)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["VehicleAcquireStarted"])
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a273")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //DoorClose,Pio완료
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "DoorClose,PIOCompleted"
                                                                                    , "회수 액션(설비 PIO 완료) "
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27b")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //PIO 시작,롤러UP
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "회수 액션(버퍼 PIO시작)"
                                                                                     , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleDepositStarted"])));
            //목적지
            if (i == 7) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            //PUT AGV -> Buffer
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "회수 액션(버퍼 PUT)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                   , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //PIO완료포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "69006307ac8ad18ebac0a27c")
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, null)));
            ////PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "회수 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, null)
                                                                                    , createPostReport(reports, ["VehicleDepositCompleted"])));
            return jobTemplate;
        }

        private JobTemplate Transport(JobTemplate jobTemplate, int i)
        {
            if (i == 6) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , false
                                                                                    , createParameta("target", null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createParameta(null, null)
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));

            ////Drop 실행
            //jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.Drop
            //                                                                        , "SL_DRUM_BUFFER_PUT_ACTION_1"
            //                                                                        , createParameta("action", "SL_DRUM_BUFFER_PUT_ACTION_1")
            //                                                                        , createParameta(null, null)
            //                                                                        , createPreReport(reports, ["VehicleDepositStarted"])
            //                                                                        , createPostReport(reports, ["CarrierRemoved", "VehicleDepositCompleted"])));

            return jobTemplate;
        }

        private MissionTemplate MissionTempleateCreateSTI(int JobTemplateId, MissionTemplateType type, MissionTemplateSubType SubType, string name, bool islock
                                                          , Parameta parameta1, Parameta parameta2, Parameta parameta3, Parameta parameta4
                                                          , List<PreReport> preReports, List<PostReport> postReports)
        {
            MissionTemplate missionTemplate = new MissionTemplate();

            //missionTemplate.jobTemplateId = JobTemplateId;
            missionTemplate.name = name;
            missionTemplate.type = type.ToString();
            missionTemplate.subType = SubType.ToString();
            missionTemplate.isLook = islock;
            missionTemplate.preReports = preReports;
            missionTemplate.postReports = postReports;
            if (parameta1 != null)
            {
                missionTemplate.parameters.Add(parameta1);
            }
            if (parameta2 != null)
            {
                missionTemplate.parameters.Add(parameta2);
            }
            if (parameta3 != null)
            {
                missionTemplate.parameters.Add(parameta3);
            }
            if (parameta4 != null)
            {
                missionTemplate.parameters.Add(parameta4);
            }

            switch (type)
            {
                case MissionTemplateType.None:
                    break;

                case MissionTemplateType.Move:
                    missionTemplate.service = Service.Worker.ToString();
                    break;

                case MissionTemplateType.Action:
                    switch (SubType)
                    {
                        case MissionTemplateSubType.None:
                        case MissionTemplateSubType.Wait:
                        case MissionTemplateSubType.Reset:
                            break;

                        case MissionTemplateSubType.Charge:
                        case MissionTemplateSubType.CHARGECOMPLETE:
                            missionTemplate.service = Service.Middleware.ToString();
                            break;

                        case MissionTemplateSubType.Cancel:
                            missionTemplate.service = Service.Middleware.ToString();
                            break;

                        case MissionTemplateSubType.Pick:
                        case MissionTemplateSubType.Drop:
                        case MissionTemplateSubType.SourceAction:
                        case MissionTemplateSubType.DestinationAction:
                            missionTemplate.service = Service.Middleware.ToString();
                            break;
                    }

                    break;
            }

            missionTemplatesSTI.Add(missionTemplate);

            return missionTemplate;
        }

        private Parameta createParameta(string paramKey, string paramValue)
        {
            Parameta param = null;
            if (paramKey != null)
            {
                param = new Parameta();
                param.key = paramKey;
                param.value = paramValue;
            }
            return param;
        }

        private List<PreReport> createPreReport(List<Report> reports, string[] eventNames)
        {
            List<PreReport> preReports = new List<PreReport>();

            if (eventNames != null)
            {
                foreach (var eventName in eventNames)
                {
                    var report = reports.FirstOrDefault(r => r.eventName == eventName);
                    if (report != null)
                    {
                        var preReport = new PreReport
                        {
                            ceid = report.ceid,
                            eventName = report.eventName,
                            rptid = report.rptid,
                        };
                        preReports.Add(preReport);
                    }
                }
            }

            return preReports;
        }

        private List<PostReport> createPostReport(List<Report> reports, string[] eventNames)
        {
            List<PostReport> postReports = new List<PostReport>();

            if (eventNames != null)
            {
                foreach (var eventName in eventNames)
                {
                    var report = reports.FirstOrDefault(r => r.eventName == eventName);
                    if (report != null)
                    {
                        var postReport = new PostReport
                        {
                            ceid = report.ceid,
                            eventName = report.eventName,
                            rptid = report.rptid,
                        };
                        postReports.Add(postReport);
                    }
                }
            }

            return postReports;
        }

        private List<Report> GetReports()
        {
            List<Report> reports = new List<Report>();

            // 전송시작
            var TransferInitiated = new Report
            {
                ceid = 208,
                eventName = "TransferInitiated",
                rptid = 3
            };
            // 차량할당
            var VehicleAssigned = new Report
            {
                ceid = 604,
                eventName = "VehicleAssigned",
                rptid = 6
            };
            // 차량출발
            var VehicleDeparted = new Report
            {
                ceid = 605,
                eventName = "VehicleDeparted",
                rptid = 6
            };
            // 차량도착
            var VehicleArrived = new Report
            {
                ceid = 601,
                eventName = "VehicleArrived",
                rptid = 6
            };
            // 전송중
            var Transferring = new Report
            {
                ceid = 211,
                eventName = "Transferring",
                rptid = 3
            };
            // 차량해제
            var VehicleUnassigned = new Report
            {
                ceid = 610,
                eventName = "VehicleUnassigned",
                rptid = 6
            };
            // 전송완료
            var TransferCompleted = new Report
            {
                ceid = 207,
                eventName = "TransferCompleted",
                rptid = 3
            };
            // 차량 PICK 시작
            var VehicleAcquireStarted = new Report
            {
                ceid = 602,
                eventName = "VehicleAcquireStarted",
                rptid = 6
            };
            // 차량 PICK 완료
            var VehicleAcquireCompleted = new Report
            {
                ceid = 603,
                eventName = "VehicleAcquireCompleted",
                rptid = 6
            };
            // 차량 DROP 시작
            var VehicleDepositStarted = new Report
            {
                ceid = 606,
                eventName = "VehicleDepositStarted",
                rptid = 6
            };
            // 차량 DROP 완료
            var VehicleDepositCompleted = new Report
            {
                ceid = 607,
                eventName = "VehicleDepositCompleted",
                rptid = 6
            };
            // 커플러 풀기 시작
            var VehicleCouplerLoosenStarted = new Report
            {
                ceid = 651,
                eventName = "VehicleCouplerLoosenStarted",
                rptid = 6
            };
            // 커플러 풀기 완료
            var VehicleCouplerLoosenComplete = new Report
            {
                ceid = 652,
                eventName = "VehicleCouplerLoosenComplete",
                rptid = 6
            };
            //  캡 고정 시작
            var VehicleCapFastenStarted = new Report
            {
                ceid = 653,
                eventName = "VehicleCapFastenStarted",
                rptid = 6
            };
            // 캡 고정 완료
            var VehicleCapFastenCompleted = new Report
            {
                ceid = 654,
                eventName = "VehicleCapFastenCompleted",
                rptid = 6
            };
            // 캡 풀기 시작
            var VehicleCapLoosenStarted = new Report
            {
                ceid = 655,
                eventName = "VehicleCapLoosenStarted",
                rptid = 6
            };
            // 캡 풀기 완료
            var VehicleCapLoosenCompleted = new Report
            {
                ceid = 656,
                eventName = "VehicleCapLoosenCompleted",
                rptid = 6
            };
            // 커플러 고정 시작
            var VehicleCouplerFastenStarted = new Report
            {
                ceid = 657,
                eventName = "VehicleCouplerFastenStarted",
                rptid = 6
            };
            // 커플러 고정 완료
            var VehicleCouplerFastenComplete = new Report
            {
                ceid = 658,
                eventName = "VehicleCouplerFastenComplete",
                rptid = 6
            };
            // 케리어 PICK
            var CarrierInstalled = new Report
            {
                ceid = 301,
                eventName = "CarrierInstalled",
                rptid = 4
            };
            // 케리어 DROP
            var CarrierRemoved = new Report
            {
                ceid = 302,
                eventName = "CarrierRemoved",
                rptid = 4
            };
            // 케리어 Id 읽기
            var CarrierIDRead = new Report
            {
                ceid = 303,
                eventName = "CarrierIDRead",
                rptid = 11
            };
            //CANCEL,Abort
            var OperatorInitiatedAction = new Report
            {
                ceid = 501,
                eventName = "OperatorInitiatedAction",
                rptid = 9
            };
            var TransferAbortCompleted = new Report
            {
                ceid = 201,
                eventName = "TransferAbortCompleted",
                rptid = 3
            };
            var TransferAbortFailed = new Report
            {
                ceid = 202,
                eventName = "TransferAbortFailed",
                rptid = 3
            };
            //CANCEL,Abort
            var TransferAbortInitiated = new Report
            {
                ceid = 203,
                eventName = "TransferAbortInitiated",
                rptid = 3
            };
            var TransferCancelCompleted = new Report
            {
                ceid = 204,
                eventName = "TransferCancelCompleted",
                rptid = 3
            };
            var TransferCancelFailed = new Report
            {
                ceid = 205,
                eventName = "TransferCancelFailed",
                rptid = 3
            };
            var TransferCancelInitiated = new Report
            {
                ceid = 206,
                eventName = "TransferCancelInitiated",
                rptid = 3
            };
            //출발지 목적지를 변경할경우
            var TransferUpdateCompleted = new Report
            {
                ceid = 212,
                eventName = "TransferUpdateCompleted",
                rptid = 3
            };

            reports.Add(TransferInitiated);
            reports.Add(VehicleAssigned);
            reports.Add(VehicleDeparted);
            reports.Add(VehicleArrived);
            reports.Add(Transferring);
            reports.Add(VehicleUnassigned);
            reports.Add(TransferCompleted);
            reports.Add(VehicleAcquireStarted);
            reports.Add(VehicleAcquireCompleted);
            reports.Add(VehicleDepositStarted);
            reports.Add(VehicleDepositCompleted);
            reports.Add(VehicleCouplerLoosenStarted);
            reports.Add(VehicleCouplerLoosenComplete);
            reports.Add(VehicleCapFastenStarted);
            reports.Add(VehicleCapFastenCompleted);
            reports.Add(VehicleCapLoosenStarted);
            reports.Add(VehicleCapLoosenCompleted);
            reports.Add(VehicleCouplerFastenStarted);
            reports.Add(VehicleCouplerFastenComplete);
            reports.Add(CarrierInstalled);
            reports.Add(CarrierRemoved);
            reports.Add(CarrierIDRead);
            reports.Add(OperatorInitiatedAction);
            reports.Add(TransferAbortCompleted);
            reports.Add(TransferAbortFailed);
            reports.Add(TransferAbortInitiated);
            reports.Add(TransferCancelCompleted);
            reports.Add(TransferCancelFailed);
            reports.Add(TransferCancelInitiated);
            reports.Add(TransferUpdateCompleted);

            return reports;
        }

        //// POST api/<JobTemplatesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<JobTemplatesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<JobTemplatesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}