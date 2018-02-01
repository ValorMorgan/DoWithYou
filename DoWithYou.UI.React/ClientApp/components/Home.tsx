import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Image, CircleImage } from './Image';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return (
            <div>
                <h1 className="homepage-title">Do With You</h1>
                <p>ToDo app with interactive progress! </p>
                <Image classes="" src="" alt="Logo" />
                <CircleImage classes="" src="" alt="Logo"/>
            </div>
        );
    }
}