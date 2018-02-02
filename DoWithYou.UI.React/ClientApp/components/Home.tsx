import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom'
import { Image, CircleImage } from './Image';
import { Title } from './Title';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return (
            <div>
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
            </div>
        );
    }
}