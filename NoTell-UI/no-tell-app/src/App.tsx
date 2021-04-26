import React, {useState} from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Col, Container, Form, Row} from "react-bootstrap";
import {
    Button, FormControl,
    FormControlLabel, FormLabel, Input, InputLabel, Radio,
    RadioGroup,
    Step,
    StepContent,
    StepLabel,
    Stepper,
    TextField
} from "@material-ui/core";
import {
    AddGuestVM,
    AddReservationGuestVM,
    AvailableRoomsForDataRangeVM,
    OpenAPI,
    ReservationService,
    RoomVM
} from "./gen";

const {postReservationService2, postReservationService1} = ReservationService;
OpenAPI.BASE = "https://localhost:44360";

const App: React.FC = () =>
{

    const steps = ['Date of reservation', 'Choose room type', 'Your details', 'Confirm', 'Success'];
    const [activeStep, setActiveStep] = React.useState(0);
    const [bedroomsNum, setbedroomsNum] = useState<number>(0);
    const [guest, setguest] = useState<AddGuestVM>( (
        {name: '', lastName: '', phone: ''}
    ));

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

    const handleRadioChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setbedroomsNum(Number(event.target.value));
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleNext = () => {
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
    };

    const handleReset = () => {
        setActiveStep(0);
    };

    const handleDateFromChange = (event: any | null) => {
        const date = new Date(event.target.value);
        setdateFrom(date);
    };

    const handleDateToChange = (event: any | null) => {
        const date = new Date(event.target.value);
        setdateTo(date);
    };

    const handleFormChange = (event: any) =>
    {
        if(guest) {
            const idFiels = event.target.id;
            const value = event.target.value;
            var tempGuest = guest;

            switch (idFiels) {
                case "my-name":
                    tempGuest.name = value;
                    break;
                case "my-lastName":
                    tempGuest.lastName = value;
                    break;
                case "my-phone":
                    tempGuest.phone = value;
                    break;
            }

            setguest(tempGuest);
        }
    }

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
                    setbedroomsNum(0);
                    handleNext();

                })
                .catch((err)=>{
                    console.log("error: ", err);
                })
        }
    }

    const validateForm = ():boolean =>
    {
        if(guest && guest.name && guest.lastName && guest.phone)
            return guest.name?.length > 0 && guest.lastName.length > 0 && guest.phone.length > 0;

        return false;
    }

    const sendReservation = () =>
    {
        var reservation: AddReservationGuestVM;
        const room = availableRooms.find(f => f.numberOfBedrooms == bedroomsNum);

        if (room) {
            reservation = {
                roomId: room.roomId,
                reservationFrom: convertDateToString(dateFrom),
                reservationTo: convertDateToString(dateTo),
                name: guest.name,
                lastName: guest.lastName,
                phone: guest.phone
            };

            postReservationService1(reservation)
                .then(() => handleNext())
                .catch((err)=> console.log("Error: ", err));
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
                            className={"form-step1_textFiels mr-4"}
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
                            <Button className={"mt-5"} variant="contained" color="primary" onClick={checkAvailableRooms}>Check available rooms</Button>
                        </div>
                    </form>
                );
            case 1:
                return (
                    <>
                    <FormControl component="fieldset" className={"my-3 mx-2"}>
                        <FormLabel component="legend">Choose number of bedrooms</FormLabel>
                        <RadioGroup aria-label="Choose number of bedrooms" name="bedroomsNum" value={bedroomsNum} onChange={handleRadioChange}>
                            {availableRoomType.map(type=>(
                                <FormControlLabel value={type} control={<Radio />} label={type} />
                            ))}
                        </RadioGroup>
                    </FormControl>
                    <div>
                        <Button variant="contained" onClick={handleBack}>
                            Back
                        </Button>
                        <Button color="primary" variant="contained" onClick={handleNext} className="mx-4" disabled={bedroomsNum == 0}>
                            Next
                        </Button>
                    </div>
                    </>
                )
            case 2:
                return (
                    <Form>
                        <FormControl className="mx-4">
                            <InputLabel htmlFor="my-name">First name</InputLabel>
                            <Input id="my-name" aria-describedby="my-helper-text" onChange={handleFormChange}/>
                        </FormControl>
                        <FormControl className="mx-4">
                            <InputLabel htmlFor="my-lastName">Last name</InputLabel>
                            <Input id="my-lastName" aria-describedby="my-helper-text" onChange={handleFormChange}/>
                        </FormControl>
                        <FormControl className="mx-4">
                            <InputLabel htmlFor="my-phone">Phone number</InputLabel>
                            <Input id="my-phone" aria-describedby="my-helper-text" onChange={handleFormChange}/>
                        </FormControl>
                        <div>
                            <Button variant="contained" onClick={handleBack}>
                                Back
                            </Button>
                            <Button color="primary" variant="contained" onClick={handleNext} className="mx-4" disabled={validateForm()}>
                                Next
                            </Button>
                        </div>
                    </Form>
                );
            case 3:
                return (
                    <div className="mx-2 mt-2">
                        <h5>Reservation from: {convertDateToString(dateFrom)} to: {convertDateToString(dateTo)}</h5>
                        <h5 className="mt-3 mb-4">
                            <span className="mr-4">First name: {guest.name}</span>
                            <span className="mx-4">Last name: {guest.lastName}</span>
                            <span className="ml-4">Phone number: {guest.phone}</span>
                        </h5>
                        <div>
                            <Button variant="contained" onClick={handleBack}>
                                Back
                            </Button>
                            <Button color="primary" variant="contained" onClick={sendReservation} className="mx-4">
                                Confirm
                            </Button>
                        </div>
                    </div>
                );
            case 4:
                return (
                    <>
                        <h4 className="text-center">Success!</h4>
                        <Button color="primary" variant="contained" onClick={handleReset}>
                            Reset
                        </Button>
                    </>
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
