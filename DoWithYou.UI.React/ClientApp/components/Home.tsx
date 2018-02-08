import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom'
import { Image, CircleImage } from './Utilities/Image';
import { Title } from './Utilities/Title';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    render() {
        return (
            <React.Fragment>
                <Link to={'/'} className='title-link'>
                    <Title id='title-homepage'>Do With You</Title>
                </Link>
                <p>ToDo app with interactive progress! </p>
                <Image src="" alt="Logo" />
                <CircleImage src="" />
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