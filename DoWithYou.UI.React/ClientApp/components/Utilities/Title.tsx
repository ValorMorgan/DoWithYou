import * as React from 'react';
import { Link, LinkProps } from 'react-router-dom';
import { ICommonProps } from './Misc';

export class Title extends React.PureComponent<ICommonProps, {}> {
    constructor(props: ICommonProps) {
        super(props);
    }

    render() {
        return (
            <h1 {...this.props} className={`title ${this.props.className ? this.props.className : ''}`.trim()}>
                {this.props.children}
            </h1>
        );
    }
}