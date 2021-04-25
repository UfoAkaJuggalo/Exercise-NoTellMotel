import React from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Col, Container, Row} from "react-bootstrap";


const App: React.FC = () =>
{

    const getSteps = (): Array<string> =>
        ['Date of reservation', 'Choose room type', 'Your details', 'Confirm']

    const getStepContent = (step: number): JSX.Element =>
    {
        switch (step) {
            case 0:
                return (
                    <p>Step 1</p>
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

             </Container>
         </>
     );
}

export default App;
