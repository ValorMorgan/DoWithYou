import * as React from 'react';
import { NavMenu } from './NavMenu';
import { Header } from './Utilities/Header';

export interface ILayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<ILayoutProps, {}> {
    public render() {
        return (
            <div className='container-fluid'>
                <div className='row no-gutters'>
                    <div className='col-sm-3'>
                        <NavMenu />
                    </div>
                    <div className='col-sm-9'>
                        <Header />
                        {this.props.children}
                    </div>
                </div>
            </div>
        );
    }
}