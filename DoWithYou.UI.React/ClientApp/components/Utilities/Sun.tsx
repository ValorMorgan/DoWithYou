import * as React from 'react';
import { ICommonProps } from './Misc';

export class Sun extends React.Component<ICommonProps, {}> {
    constructor(props: ICommonProps) {
        super(props);
    }

    render() {
        // TODO: Make sun svg image
        return (
            <img {...this.props} src="" className={`sun ${this.props.className ? this.props.className : ''}`.trim()} />
        );
    }
}