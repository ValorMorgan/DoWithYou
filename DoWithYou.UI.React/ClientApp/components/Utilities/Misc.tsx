import * as React from 'react'

export interface ICommonProps {
    id?: string;
    className?: string;
    onClick?: (string | any);
}

export const ClearFix = () =>
    <div className="clearfix"></div>;

export class Icon extends React.PureComponent<{icon: string}, {}> {
    constructor(props: {icon: string}) {
        super(props);
    }

    render() {
        return (
            <i className="icon material-icons">{this.props.icon}</i>
        );
    }
}