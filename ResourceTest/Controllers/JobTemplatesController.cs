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


        // GET api/<JobTemplatesController>/5
        [HttpGet("STI")]
        public ActionResult<List<JobTemplate>> GetSTI()
        {
            reports = GetReports();
            jobTemplatesSTI.Clear();
            missionTemplatesSTI.Clear();

            for (int id = 1; id <= 11; id++)
            {
                var jobTemplate = BuildStiJobTemplate(id);
                if (jobTemplate != null)
                {
                    jobTemplatesSTI.Add(jobTemplate);
                }
            }

            return jobTemplatesSTI;
        }

        private JobTemplate BuildStiJobTemplate(int id)
        {
            var jobTemplate = new JobTemplate
            {
                id = id,
                group = "",
            };

            switch (id)
            {
                case 1:
                    jobTemplate.type = JobTemplateType.Move.ToString();
                    jobTemplate.subType = JobTemplateSubType.SimpleMove.ToString();
                    AddDefaultStiMoveMission(jobTemplate, id, MissionTemplateSubType.DestinationMove, false);
                    return jobTemplate;

                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    return BuildStiTransportJobTemplate(jobTemplate, id);

                case 8:
                    jobTemplate.type = JobTemplateType.Transport.ToString();
                    jobTemplate.subType = JobTemplateSubType.DropOnly.ToString();
                    return Transport(jobTemplate, id);

                case 9:
                    jobTemplate.type = JobTemplateType.Charge.ToString();
                    jobTemplate.subType = JobTemplateSubType.Charge.ToString();
                    return BuildStiChargeJobTemplate(jobTemplate, id);

                case 10:
                    jobTemplate.type = JobTemplateType.Wait.ToString();
                    jobTemplate.subType = JobTemplateSubType.Wait.ToString();
                    AddDefaultStiMoveMission(jobTemplate, id, MissionTemplateSubType.DestinationMove, false);
                    return jobTemplate;

                case 11:
                    jobTemplate.type = JobTemplateType.Reset.ToString();
                    jobTemplate.subType = JobTemplateSubType.Reset.ToString();
                    AddDefaultStiMoveMission(jobTemplate, id, MissionTemplateSubType.ResetMove, false);
                    return jobTemplate;

                default:
                    return null;
            }
        }

        private JobTemplate BuildStiTransportJobTemplate(JobTemplate jobTemplate, int id)
        {
            jobTemplate.subType = JobTemplateSubType.PickDrop.ToString();

            switch (id)
            {
                case 2:
                    jobTemplate.type = JobTemplateType.TRANSPORT_CHEMICAL_SUPPLY.ToString();
                    return TransportChemicalSupply(jobTemplate, id);

                case 3:
                    jobTemplate.type = JobTemplateType.TRANSPORT_CHEMICAL_RECOVERY.ToString();
                    return TransportChemicalRecovery(jobTemplate, id);

                case 4:
                    jobTemplate.type = JobTemplateType.TRANSPORT_SLURRY_SUPPLY.ToString();
                    return TransportSlurrySupply(jobTemplate, id);

                case 5:
                    jobTemplate.type = JobTemplateType.TRANSPORT_SLURRY_RECOVERY.ToString();
                    return TransportSlurryRecovery(jobTemplate, id);

                case 6:
                    jobTemplate.type = JobTemplateType.TRANSPORT_AICERO_SUPPLY.ToString();
                    return TransportAicellomilimSupply(jobTemplate, id);

                case 7:
                    jobTemplate.type = JobTemplateType.TRANSPORT_AICERO_RECOVERY.ToString();
                    return TransportAicellomilimRecovery(jobTemplate, id);

                default:
                    return jobTemplate;
            }
        }

        private void AddDefaultStiMoveMission(JobTemplate jobTemplate, int jobTemplateId, MissionTemplateSubType subType, bool isLock)
        {
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(
                jobTemplateId,
                MissionTemplateType.Move,
                subType,
                null,
                isLock,
                createParameta("target", null),
                null,
                null,
                null,
                null,
                null));
        }

        private JobTemplate BuildStiChargeJobTemplate(JobTemplate jobTemplate, int id)
        {
            AddDefaultStiMoveMission(jobTemplate, id, MissionTemplateSubType.DestinationMove,true);
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(
                id,
                MissionTemplateType.Action,
                MissionTemplateSubType.Charge,
                "Charge",
                false,
                createParameta("action", "Charge"),
                null,
                null,
                null,
                null,
                null));

            return jobTemplate;
        }

        private JobTemplate TransportChemicalSupply(JobTemplate jobTemplate, int i)
        {
            //버퍼 대기 포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf22")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));

            //출발지이동
            if (i == 2) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //PIO완료 위치 이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf23")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,롤러다운 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "공급 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //2DrumCapSearch
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "CapSearch_2Drum"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf2c")
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));
            //,Cap정렬실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 CAP 정렬)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_ALIGN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf00")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,DoorOpen"
                                                                                    , "공급 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //목적지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));

            //설비바코드리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading"
                                                                                    , "공급 액션(설비 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_EQP_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //드럼이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "공급 액션(설비 DRUM 이동)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf28")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPreReport(reports, ["VehicleDepositCompleted"])
                                                                                    , null));
            //UR작업[CapOpen,커플러체결]
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 CAP 열기)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_OPEN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 COUPLER 체결)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_COUPLER_ATTACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf1b")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,Close
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted"
                                                                                    , "공급 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));
            return jobTemplate;
        }

        private JobTemplate TransportChemicalRecovery(JobTemplate jobTemplate, int i)
        {
            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf00")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf28")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //커플러해제,CapClose
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "Capclose"
                                                                                    , "회수 액션(설비 COUPLER 분리)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_COUPLER_DETACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                 //, "Capclose"
                                                                                 , "회수 액션(설비 CAP 닫기)"
                                                                                 , true
                                                                                 , createParameta("type", "CDRUM")
                                                                                 , createParameta("linkedFacility", null)
                                                                                 , createParameta("action", "GET_ACTION_CAP_CLOSE")
                                                                                 , createParameta("drumKeyCode", null)
                                                                                 , null
                                                                                 , null));

            //출발지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));

            ////Get
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
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf1b")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //DoorClose,Pio완료
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "DoorClose,PIOCompleted"
                                                                                    , "회수 액션(설비 PIO 완료) "
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_NoTrun"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf2d")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf22")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////PIO 시작,롤러UP
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "회수 액션(버퍼 PIO시작)"
                                                                                     , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleDepositStarted"])));

            //목적지
            if (i == 3) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PUT AGV -> Buffer
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "회수 액션(버퍼 PUT)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                   , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //PIO완료포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf23")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            ////PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "회수 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "CDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleDepositCompleted"])));
            return jobTemplate;
        }

        private JobTemplate TransportSlurrySupply(JobTemplate jobTemplate, int i)
        {
            //버퍼 대기 포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf22")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));

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
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf23")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));

            ///PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "공급 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //SlurryCapSearch
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "CapSearch"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf10")
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                //, "Barcodereading,Capalignment"
                                                                                , "공급 액션(드럼 CAP 정렬)"
                                                                                , true
                                                                                , createParameta("type", "SDRUM")
                                                                                , createParameta("linkedFacility", null)
                                                                                , createParameta("action", "PUT_ACTION_CAP_ALIGN")
                                                                                , createParameta("drumKeyCode", null)
                                                                                , null
                                                                                , null));

            //Slurry_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf11")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,Dooropen"
                                                                                    , "공급 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //목적지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //설비바코드리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading"
                                                                                    , "공급 액션(설비 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_EQP_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //드럼이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "공급 액션(설비 DRUM 이동)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //Slurry_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cefc")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPreReport(reports, ["VehicleDepositCompleted"])
                                                                                    , null));
            //UR작업[CapOpen,커플러체결]
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 CAP 열기)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_OPEN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 COUPLER 체결)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_COUPLER_ATTACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //Slurry_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Slurry_4(End)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf05")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,Close
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted"
                                                                                    , "공급 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));
            return jobTemplate;
        }

        private JobTemplate TransportSlurryRecovery(JobTemplate jobTemplate, int i)
        {
            //Slurry_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "Slurry_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf11")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));

            //Slurry_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "Slurry_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cefc")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            ////커플러해제,CapClose
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "CapClose"
                                                                                    , "회수 액션(설비 COUPLER 분리)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_COUPLER_DETACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                              //, "CapClose"
                                                                              , "회수 액션(설비 CAP 닫기)"
                                                                              , true
                                                                              , createParameta("type", "SDRUM")
                                                                              , createParameta("linkedFacility", null)
                                                                              , createParameta("action", "GET_ACTION_CAP_CLOSE")
                                                                              , createParameta("drumKeyCode", null)
                                                                              , null
                                                                              , null));

            ////출발지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                            , null
                                                                            , true
                                                                            , createParameta("target", null)
                                                                            , null
                                                                            , null
                                                                            , null
                                                                            , null
                                                                            , null));

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


            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf1b")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //DoorClose,Pio완료
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "DoorClose,PIOCompleted"
                                                                                    , "회수 액션(설비 PIO 완료) "
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf22")
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                     , null));

            //목적지
            if (i == 5) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
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
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf23")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "회수 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "SDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                     , createPostReport(reports, ["VehicleDepositCompleted"])));

            return jobTemplate;
        }

        private JobTemplate TransportAicellomilimSupply(JobTemplate jobTemplate, int i)
        {
            //버퍼 대기 포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf22")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));

            //출발지이동
            if (i == 6) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierInstalled"])));

            //PIO완료 위치 이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf23")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,롤러다운 실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "공급 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));

            //2DrumCapSearch
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "CapSearch_2Drum"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf2c")
                                                                                    , null
                                                                                    , null
                                                                                    , null
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
                                                                                    , null));
            //,Cap정렬실행
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading,Capalignment"
                                                                                    , "공급 액션(드럼 CAP 정렬)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_ALIGN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf00")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,DoorOpen"
                                                                                    , "공급 액션(설비 PIO 시작)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //목적지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));

            //설비바코드리딩
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "Barcodereading"
                                                                                    , "공급 액션(설비 BARCODE 읽기)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_EQP_BCR_READ")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //드럼이동
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "공급 액션(설비 DRUM 이동)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf28")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPreReport(reports, ["VehicleDepositCompleted"])
                                                                                    , null));
            //UR작업[CapOpen,커플러체결]
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 CAP 열기)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_CAP_OPEN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "URStart[CapOpen]"
                                                                                    , "공급 액션(설비 COUPLER 체결)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_COUPLER_ATTACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_5(Door close)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf1b")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,Close
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted"
                                                                                    , "공급 액션(설비 PIO 완료)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));
            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf00")
                                                                                    , null
                                                                                    , null
                                                                                    , null  
                                                                                    , null
                                                                                    , null));
            //2Drum_5(Door close)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "Parking_3"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf26")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            return jobTemplate;
        }

        private JobTemplate TransportAicellomilimRecovery(JobTemplate jobTemplate, int i)
        {
            //2Drum_4(Door open)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_4(Door open)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf00")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            //PIO 시작,DoorOpen
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "PIOStart,DoorOpen"
                                                                                    , "회수 액션(설비 PIO 시작)"
                                                                                   , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , createPreReport(reports, ["Transferring"])
                                                                                    , null));

            //2Drum_2(UR)
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceStopoverMove
                                                                                    , "2Drum_2(UR)"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf28")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //커플러해제,CapClose
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "CapClose"
                                                                                    , "회수 액션(설비 COUPLER 분리)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_COUPLER_DETACH")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , null));

            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                              //, "CapClose"
                                                                              , "회수 액션(설비 CAP 닫기)"
                                                                              , true
                                                                              , createParameta("type", "IDRUM")
                                                                              , createParameta("linkedFacility", null)
                                                                              , createParameta("action", "GET_ACTION_CAP_CLOSE")
                                                                              , createParameta("drumKeyCode", null)
                                                                              , null
                                                                              , null));

            ////출발지
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.SourceMove
                                                                            , null
                                                                            , true
                                                                            , createParameta("target", null)
                                                                            , null
                                                                            , null
                                                                            , null
                                                                            , null
                                                                            , null));

            ////Get
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
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf1b")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //DoorClose,Pio완료
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.SourceAction
                                                                                    //, "DoorClose,PIOCompleted"
                                                                                    , "회수 액션(설비 PIO 완료) "
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "GET_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleAcquireCompleted"])));
            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "2Drum_NoTrun"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf2d")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //버퍼대기포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerUp"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf22")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));
            ////PIO 시작,롤러UP
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOStart,RollerUp"
                                                                                    , "회수 액션(버퍼 PIO시작)"
                                                                                     , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_BEGIN")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleDepositStarted"])));

            //목적지
            if (i == 7) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , true
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));

            ////PUT
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PUTDrum"
                                                                                    , "회수 액션(버퍼 PUT)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_DRUM_MOVE")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["CarrierRemoved"])));

            //PIO완료포지션
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationStopoverMove
                                                                                    , "IdlerDown"
                                                                                    , true
                                                                                    , createParameta("target", "699e7b3e841e04e64997cf23")
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , null));
            //PIO완료,롤러다운
            jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.DestinationAction
                                                                                    //, "PIOCompleted,Rollerdown"
                                                                                    , "회수 액션(버퍼 PIO완료)"
                                                                                    , true
                                                                                    , createParameta("type", "IDRUM")
                                                                                    , createParameta("linkedFacility", null)
                                                                                    , createParameta("action", "PUT_ACTION_PIO_END")
                                                                                    , createParameta("drumKeyCode", null)
                                                                                    , null
                                                                                    , createPostReport(reports, ["VehicleDepositCompleted"])));
            return jobTemplate;
        }

        private JobTemplate Transport(JobTemplate jobTemplate, int i)
        {
            if (i == 6) jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Move, MissionTemplateSubType.DestinationMove
                                                                                    , null
                                                                                    , false
                                                                                    , createParameta("target", null)
                                                                                    , null
                                                                                    , null
                                                                                    , null
                                                                                    , createPreReport(reports, ["VehicleDeparted"])
                                                                                    , createPostReport(reports, ["VehicleArrived"])));

            ////Drop 실행
            //jobTemplate.missionTemplates.Add(MissionTempleateCreateSTI(i, MissionTemplateType.Action, MissionTemplateSubType.Drop
            //                                                                        , "SL_DRUM_BUFFER_PUT_ACTION_1"
            //                                                                        , createParameta("action", "SL_DRUM_BUFFER_PUT_ACTION_1")
            //                                                                        , null
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

            if (parameta1 == null && parameta2 == null && parameta3 == null && parameta4 == null)
            {
                missionTemplate.parameters = null;
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
