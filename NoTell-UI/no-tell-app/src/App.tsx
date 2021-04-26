import React, {useState} from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Col, Container, Row} from "react-bootstrap";
import {
    Button,
    FormControlLabel, Radio,
    RadioGroup,
    Step,
    StepContent,
    StepLabel,
    Stepper,
    TextField
} from "@material-ui/core";
import {AvailableRoomsForDataRangeVM, OpenAPI, ReservationService, RoomVM} from "./gen";

const {postReservationService2} = ReservationService;
OpenAPI.BASE = "https://localhost:44360";

interface AvailableRoom {
    numberOfBedrooms: number,
    avalibality: number
}

const App: React.FC = () =>
{

    const steps = ['Date of reservation', 'Choose room type', 'Your details', 'Confirm'];
    const [activeStep, setActiveStep] = React.useState(0);
    const [bedroomsNum, setbedroomsNum] = useState<number>(0);

    const [availableRoomType, setavailableRoomType] = useState<Array<number>>(
        []
    );
    const [availableRooms, setavailableRooms] = useState<Array<RoomVM>>(
        []
    );

    const [dateFrom, setdateFrom] = React.useState<Date>(
        new Date(),
    );

    const [dateTo, setdateTo] = React.useState<Date>(
        ():Date =>{
            let date = new Date();
            date.setHours(date.getHours()+24);
            return date;
        }
    );

    const convertDateToString = (date: Date): string =>
        [date.getFullYear(), date.getMonth() + 1, date.getDate()]
            .map(n => n < 10 ? `0${n}` : `${n}`).join('-')

    const getDateNow = (tomorrow:boolean): string=>
    {
        let date = new Date();

        if(tomorrow)
            date.setHours(date.getHours()+24);

        return convertDateToString(date);
    }
    const handleDateFromChange = (event: any | null) => {
        const date = new Date(event.target.value);
        setdateFrom(date);
    };

    const handleDateToChange = (event: any | null) => {
        const date = new Date(event.target.value);
        setdateTo(date);
    };

    const checkAvailableRooms = () =>
    {
        if (dateTo > dateFrom)
        {
            let sendData:AvailableRoomsForDataRangeVM = {
                reservationFrom: convertDateToString(dateFrom),
                reservationTo: convertDateToString(dateTo)
            };

            postReservationService2(sendData)
                .then((result) => {
                    var tempArray:Array<number> = [];

                    result.forEach(val =>
                    {
                        if (val.numberOfBedrooms != undefined && tempArray.indexOf(val.numberOfBedrooms) < 0)
                            tempArray.push(val.numberOfBedrooms);
                    });

                    setavailableRooms(result);
                    setavailableRoomType(tempArray);
                    setActiveStep((step) => step + 1);

                })
                .catch((err)=>{
                    console.log("error: ", err);
                })
        }
    }

    const getStepContent = (step: number): JSX.Element =>
    {
        switch (step) {
            case 0:
                return (
                    <form className={"my-3 mx-2"} noValidate>
                        <TextField
                            id="date-from-picker"
                            label="Pick a start date of your reservation"
                            type="date"
                            className={"form-step1_textFiels mx-4"}
                            defaultValue={getDateNow(false)}
                            onChange={handleDateFromChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                        <TextField
                            id="date-to-picker"
                            label="Pick a end date of your reservation"
                            type="date"
                            className={"form-step1_textFiels mx-4"}
                            defaultValue={getDateNow(true)}
                            onChange={handleDateToChange}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                        <div>
                            <Button className={"mt-5 ml-4"} variant="contained" color="primary" onClick={checkAvailableRooms}>Check available rooms</Button>
                        </div>
                    </form>
                );
            case 1:
                return (
                    <p>Step 2</p>
                );
            case 2:
                return (
                    <p>Step 3</p>
                );
            case 3:
                return (
                    <p>Step 4</p>
                );
        }
        return (<p>Unknown step</p>);
    }

     return (
         <>
             <header>
                 <Container>
                     <Row className={"align-items-center mt-5 justify-content-center"}>
                         <Col xl={6}>
                             <h1>No Tell Motel</h1>
                             <h4>Reservation system</h4>
                         </Col>
                         <Col xl={2} className={"ml-4"}>
                             <img src="img/logo.png" width={"150rem"} alt={"logo"}/>
                         </Col>
                     </Row>
                 </Container>
             </header>
             <Container>
                 <Stepper activeStep={activeStep} orientation="vertical" className={"mt-5"}>
                     {steps.map((label, index) =>(
                         <Step key={label}>
                             <StepLabel>{label}</StepLabel>
                             <StepContent>
                                 {getStepContent(index)}
                             </StepContent>
                         </Step>
                     ))}
                 </Stepper>
             </Container>
         </>
     );
}

export default App;
