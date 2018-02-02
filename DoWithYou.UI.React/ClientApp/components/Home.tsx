import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom'
import { Image, CircleImage } from './Image';
import { Title } from './Title';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return (
            <div>
                <Link to={'/'}><Title id='title-homepage'>Do With You</Title></Link>
                <p>ToDo app with interactive progress! </p>
                <Image src="" alt="Logo" />
                <CircleImage src="" />
            </div>
        );
    }
}