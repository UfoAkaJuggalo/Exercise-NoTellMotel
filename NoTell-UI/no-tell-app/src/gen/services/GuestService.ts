/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddGuestVM } from '../models/AddGuestVM';
import type { GuestVM } from '../models/GuestVM';
import { request as __request } from '../core/request';

export class GuestService {

    /**
     * @param requestBody 
     * @returns number Success
     * @throws ApiError
     */
    public static async postGuestService(
requestBody?: AddGuestVM,
): Promise<number> {
        const result = await __request({
            method: 'POST',
            path: `/Guest`,
            body: requestBody,
            errors: {
                500: `Server Error`,
            },
        });
        return result.body;
    }

    /**
     * @param id 
     * @returns GuestVM Success
     * @throws ApiError
     */
    public static async getGuestService(
id?: number,
): Promise<GuestVM> {
        const result = await __request({
            method: 'GET',
            path: `/Guest`,
            query: {
                'id': id,
            },
            errors: {
                500: `Server Error`,
            },
        });
        return result.body;
    }

}