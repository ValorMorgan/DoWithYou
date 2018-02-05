import * as React from 'react';

export class Button extends React.Component<{}, {}> {
    constructor() {
        super();
    }

    public render() {
        return (
            <button className='button'>{this.props.children}</button>
        );
    }
}