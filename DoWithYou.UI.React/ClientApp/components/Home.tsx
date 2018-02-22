import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom'
import { Image, CircleImage } from './Utilities/Image';
import { Title } from './Utilities/Title';
import { DigitalClock, SunClock } from './Utilities/Clock';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    render() {
        return (
            <React.Fragment>
                <div id="dayNight">
                    <SunClock />
                </div>
                <DigitalClock />
                <Link to={'/'} className='title-link'>
                    <Title id='title-homepage'>Do With You</Title>
                </Link>
                <p>ToDo app with interactive progress! </p>
                <Image src="" alt="Super long alt name for some reason we could never know why" />
                <CircleImage src="" alt="Super long alt name for some reason we could never know why" />
                <ul>
                    <li>First</li>
                    <li><span>Second</span></li>
                </ul>
                <ol>
                    <li>First</li>
                    <li><span>Second</span></li>
                </ol>
            </React.Fragment>
        );
    }
}