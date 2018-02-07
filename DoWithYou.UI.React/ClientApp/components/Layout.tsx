import * as React from 'react';
import * as Misc from './Utilities/Misc';
import { NavMenu } from './NavMenu';
import { Header } from './Utilities/Header';

export interface ILayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<ILayoutProps, {}> {
    public render() {
        return (
            <div className='container-fluid'>
                <div className='row'>
                    <div className='col-sm-2'>
                        <NavMenu />
                    </div>
                    <Misc.ClearFix />
                    <div className='col-sm-10'>
                        <Header />
                        {this.props.children}
                    </div>
                </div>
            </div>
        );
    }
}