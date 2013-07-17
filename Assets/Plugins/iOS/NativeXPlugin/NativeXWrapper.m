//
//  W3iWrapper.cpp
//  Unity-iPhone
//
//  Created by Josh Ruis on 1/18/13.
//
//

#import "NativeXCore.h"
#import "NativeXMonetizationSDK.h"

#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

#define GetStringParamOrNil( _x_ ) ( _x_ != NULL && strlen( _x_ ) ) ? [NSString stringWithUTF8String:_x_] : nil

void uStartWithNameAndApplicationId(const char *name, const char *appId, const char *pubId, bool enableLogging)
{
    [[NativeXCore instance] startWithName:GetStringParam(name) applicationId:GetStringParamOrNil(appId) publisherId:GetStringParamOrNil(pubId) enableLogging:enableLogging];
    
}

void uSelectServer(const char *url)
{
    [[NativeXCore instance] setURL:GetStringParamOrNil(url)];
}

void uSetCoordinates(float bannerX, float bannerY, float bannerHeight, float bannerWidth, float offerWallX, float offerWallY)
{
    [[NativeXCore instance] setBannerPoint:CGPointMake(bannerX, bannerY)];
    [[NativeXCore instance] setOfferWallPoint:CGPointMake(offerWallX, offerWallY)];
    [[NativeXCore instance] setBannerHeight:bannerHeight];
    [[NativeXCore instance] setBannerWidth:bannerWidth];
    
}
void uShowOfferWall()
{
    [[NativeXCore instance] showOfferWall];
}

void uShowIncentOfferWall()
{
    [[NativeXCore instance] showIncentOfferWall];
}

void uShowNonIncentOfferWall()
{
    [[NativeXCore instance] showNonIncentOfferWall];
}

void uShowOfferWallFromPoint()
{
    [[NativeXCore instance] showOfferWallFromPoint];
}

void uGetAndCacheFeaturedOffer()
{
    [[NativeXCore instance]getAndCacheFeaturedOffer];
}

void uShowCachedFeaturedOffer()
{
    [[NativeXCore instance] showCachedFeaturedOffer];
}

void uShowFeaturedOffer()
{
    [[NativeXCore instance] showFeaturedOffer];
}

void uFetchInterstitial(const char* name)
{
    [[NativeXCore instance] fetchInterstitial:GetStringParamOrNil(name)];
}

void uShowInterstitial(const char* name)
{
    [[NativeXCore instance] showInterstitial:GetStringParamOrNil(name)];
}

void uConnectWithAppId(const char *appId)
{
    [[NativeXCore instance] connectWithAppId:GetStringParamOrNil(appId) ];
}

void uActionTakenWithActionId(const char *actionId)
{
    [[NativeXCore instance]actionTaken:GetStringParamOrNil(actionId)];
}


void uRedeemCurrency()
{
    [[NativeXCore instance] redeemCurrency];
}

void uTrackInAppPurchase(const char *storeProdId, const char *storeTransId, float cost, int quantity, const char *prodTitle)
{
    NativeXInAppPurchaseTrackRecord *myRecord = [[NativeXInAppPurchaseTrackRecord alloc] init];
    
    myRecord.storeProductID = GetStringParam(storeProdId);
    myRecord.storeTransactionID = GetStringParam(storeTransId);
    myRecord.costPerItem = [[NSDecimalNumber alloc] initWithFloat:cost];
    myRecord.quantity = quantity;
    myRecord.productTitle = GetStringParam(prodTitle);
    myRecord.currencyLocale = [NSLocale currentLocale];
    myRecord.storeTransactionTime = [NSDate date];
    
    [[NativeXCore instance]trackInAppPurchase:myRecord];
}

void uClose()
{
    [[NativeXCore instance] close];
}