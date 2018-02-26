import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom'
import { Image, CircleImage } from './Utilities/Image';
import { Title } from './Utilities/Title';
import { DigitalClock } from './Utilities/Clock';
import { DayNight } from './Utilities/DayNight';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    render() {
        return (
            <React.Fragment>
                <DayNight />
                <Link to={'/'} className='title-link'>
                    <Title id='title-homepage'>Do With You</Title>
                </Link>
            </React.Fragment>
        );
    }
}