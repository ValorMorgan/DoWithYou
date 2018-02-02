import * as React from 'react';
import { Link, LinkProps } from 'react-router-dom';

interface ITitleProps {
    id?: string;
    className?: string;
}

interface ITitleState extends ITitleProps {

}

// TITLE ==============================================================================
export class Title extends React.Component<ITitleProps, ITitleState> {
    constructor(props: ITitleProps) {
        super(props);
        
        this.state = {
            id: this.props.id,
            className: this.props.className ? `title ${this.props.className}` : 'title'
        };
    }

    render() {
        return (
            <h1 {...this.state}>
                {this.props.children}
            </h1>
        );
    }
}